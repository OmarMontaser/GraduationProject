import os
import librosa
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import accuracy_score
import joblib

import os
import pandas as pd
from scipy.io import wavfile
from sklearn.metrics import accuracy_score



def extract_features(file_path):
    try:
        audio, sample_rate = librosa.load(file_path, res_type='kaiser_fast') 
        mfccs = np.mean(librosa.feature.mfcc(y=audio, sr=sample_rate, n_mfcc=40).T, axis=0)
        return mfccs
    except Exception as e:
        print("Error encountered while parsing file:", file_path)
        return None


def load_data(data_dir):
    fake_files = [os.path.join(data_dir, "fake", f) for f in os.listdir(os.path.join(data_dir, "fake")) if f.endswith(".wav")]
    real_files = [os.path.join(data_dir, "real", f) for f in os.listdir(os.path.join(data_dir, "real")) if f.endswith(".wav")]

    fake_labels = [0] * len(fake_files)
    real_labels = [1] * len(real_files)

    files = fake_files + real_files
    labels = fake_labels + real_labels

    return files, labels

files, labels = load_data("xtest")

# Veri setini eğitim ve test setlerine ayırma
X_train, X_test, y_train, y_test = train_test_split(files, labels, test_size=0.2, random_state=42)

# Ses dosyalarını özellik matrisine dönüştürme
X_train = [extract_features(file) for file in X_train]
X_test = [extract_features(file) for file in X_test]

X_train = [x for x in X_train if x is not None]
X_test = [x for x in X_test if x is not None]


loaded_model = joblib.load("models/random_forest_model.joblib")

loaded_model.fit(X_train , y_train)

# example_features = extract_features(example_file_path)
# if example_features is not None:
prediction = loaded_model.predict(X_test)

accuracy = accuracy_score(prediction, y_test)
print("Accuracy:", accuracy)


model_filename = "random_forest_model.joblib"
joblib.dump(loaded_model, model_filename)
print(f"Model saved as {model_filename}")
