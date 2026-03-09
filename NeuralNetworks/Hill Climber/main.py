import matplotlib.pyplot as plt
import numpy as np

class LinearHillClimber:
    def __init__(self, learning_rate=0.01):
        self.learning_rate = learning_rate
        self.weights = None

    def fit(self, X, y, epochs=1000):
        num_samples, num_features = X.shape
        self.weights = np.random.rand(num_features)

        for epoch in range(epochs):
            predictions = self.predict(X)
            errors = y - predictions
            self.weights += self.learning_rate * np.dot(X.T, errors)

    def predict(self, X):
        return np.dot(X, self.weights)