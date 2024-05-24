# from flask import Flask ,request 
# app = Flask(__name__)

#code here




from flask import Flask, request, render_template
from test import runtest
app = Flask(__name__)

@app.route('/UploadVoices', methods=['GET', 'POST'])
def upload_voices():

    runtest(example_file_path)




if __name__ == "__main__":
    app.run(debug=True)






# from flask import Flask, request, jsonify
# import os
# import librosa
# import numpy as np
# from sklearn.externals import joblib

# app = Flask(__name__)

# # Load the pre-trained model
# loaded_model = joblib.load("models/random_forest_model.joblib")

# def extract_features(file_path):
#     try:
#         audio, sample_rate = librosa.load(file_path, res_type='kaiser_fast') 
#         mfccs = np.mean(librosa.feature.mfcc(y=audio, sr=sample_rate, n_mfcc=40).T, axis=0)
#         return mfccs
#     except Exception as e:
#         print("Error encountered while parsing file:", file_path)
#         return None

# @app.route('/predict', methods=['POST'])
# def predict():
#     if 'file' not in request.files:
#         return jsonify({'error': 'No file part'})
    
#     audio_file = request.files['file']
    
#     if audio_file.filename == '':
#         return jsonify({'error': 'No selected file'})
    
#     file_path = 'temp_audio.wav'  # Save the audio temporarily
#     audio_file.save(file_path)
    
#     example_features = extract_features(file_path)
    
#     if example_features is not None:
#         prediction = loaded_model.predict([example_features])
#         class_label = "Real" if prediction[0] == 1 else "Fake"
#         return jsonify({'prediction': class_label})
#     else:
#         return jsonify({'error': 'Error extracting features from the example file.'})

# if __name__ == '__main__':
#     app.run(debug=True)



