from flask import Flask, request, jsonify
import os
import librosa
import numpy as np
from sklearn.ensemble import RandomForestClassifier
import joblib
import tempfile
import shutil

app = Flask(__name__)

# Load the model
model = joblib.load("models/random_forest_model.joblib")

# Define a different target directory path
directory_path = r"D:\Temp\Voices"  # Change this to a directory you have confirmed write permissions

# Ensure the target directory exists
os.makedirs(directory_path, exist_ok=True)

def extract_features(file_path):
    try:
        audio, sample_rate = librosa.load(file_path, res_type='kaiser_fast')
        mfccs = np.mean(librosa.feature.mfcc(y=audio, sr=sample_rate, n_mfcc=40).T, axis=0)
        return mfccs
    except Exception as e:
        print(f"Error encountered while parsing file: {file_path}")
        return None

@app.route('/predict', methods=['POST'])
def predict():
    if 'file' not in request.files:
        return jsonify({"error": "No file part"})
    
    file = request.files['file']
    
    if file.filename == '':
        return jsonify({"error": "No selected file"})
    
    # Create a temporary file
    with tempfile.NamedTemporaryFile(delete=False) as temp_file:
        file.save(temp_file.name)
        temp_file_path = temp_file.name

    # Construct the new file path
    target_file_path = os.path.join(directory_path, file.filename)
    
    # Move the temporary file to the target directory
    try:
        shutil.move(temp_file_path, target_file_path)
    except Exception as e:
        return jsonify({"error": f"Could not move file to target directory: {str(e)}"})

    features = extract_features(target_file_path)
    if features is not None:
        prediction = model.predict([features])
        class_label = "Real" if prediction[0] == 1 else "Fake"
        return jsonify({"result": f"{class_label} Audio File"})
    else:
        return jsonify({"error": "Error extracting features from the file"})

if __name__ == '__main__':
    app.run(debug=True, port=5000)
