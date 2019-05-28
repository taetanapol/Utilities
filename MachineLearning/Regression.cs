﻿using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using MachineLearning.Shared;
using MachineLearning.Shared.Attributes;

namespace MachineLearning
{
    public static class Regression
    {
        private static TransformerChain<TTrainer> RegressionTrainerTemplate<TType, TTrainer>(this MLContext context, IEnumerable<TType> trainDataset, IEstimator<TTrainer> estimator)
        where TType : class, new()
        where TTrainer : class, ITransformer
        {
            var type = typeof(TType);
            var labelColumnName = Preprocessing.LabelColumn(type.GetProperties()).Name;
            var properties = Preprocessing.ExcludeColumns(type.GetProperties());

            var preprocessor = context.OneHotEncoding(properties);
            var trainDataframe = context.Data.LoadFromEnumerable(trainDataset);

            var pipeline = context.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: labelColumnName)
                .Append(preprocessor.OneHotEncodingEstimator)
                .Append(context.Transforms.Concatenate("Features", preprocessor.CombinedFeatures.ToArray()))
                .Append(context.Transforms.ProjectToPrincipalComponents(outputColumnName: "PCAFeatures", inputColumnName: "Features", rank: 2))
                .Append(estimator);
            var model = pipeline.Fit(trainDataframe);
            return model;
        }
        public static PredictionEngine<TIn, TOut> StochasticDoubleCoordinateAscent<TIn, TOut>(
            IEnumerable<TIn> trainDataset,
            string exampleWeightColumnName = null,
            ISupportSdcaRegressionLoss lossFunction = null,
            float? l1Regularization = null,
            float? l2Regularization = null,
            int? maximumNumberOfIterations = null,
            Action<ITransformer> additionModelAction = null)
    where TIn : class, new()
    where TOut : class, new()
        {
            var context = new MLContext();
            var model = context.RegressionTrainerTemplate(trainDataset, context.Regression.Trainers.Sdca(
                    labelColumnName: "Label",
                    featureColumnName: "Features",
                    exampleWeightColumnName: exampleWeightColumnName,
                    lossFunction: lossFunction,
                    l1Regularization: l1Regularization,
                    l2Regularization: l2Regularization,
                    maximumNumberOfIterations: maximumNumberOfIterations
                ));
            var predictEngine = context.Model.CreatePredictionEngine<TIn, TOut>(model);
            additionModelAction?.Invoke(model);
            return predictEngine;
        }
        public static PredictionEngine<TIn, TOut> LbfgsPoisson<TIn, TOut>(
            IEnumerable<TIn> trainDataset,
            string exampleWeightColumnName = null,
            float l1Regularization = 1,
            float l2Regularization = 1,
            float optimizationTolerance = (float)1E-07,
            int historySize = 20,
            bool enforceNonNegativity = false,
            Action<ITransformer> additionModelAction = null
            ) where TIn : class, new() where TOut : class, new()
        {
            var context = new MLContext();
            var model = context.RegressionTrainerTemplate(trainDataset, context.Regression.Trainers.LbfgsPoissonRegression(
                    labelColumnName: "Label",
                    featureColumnName: "Features",
                    exampleWeightColumnName: exampleWeightColumnName,
                    l1Regularization: l1Regularization,
                    l2Regularization: l2Regularization,
                    optimizationTolerance: optimizationTolerance,
                    historySize: historySize,
                    enforceNonNegativity: enforceNonNegativity
                    ));

            var predictEngine = context.Model.CreatePredictionEngine<TIn, TOut>(model);
            additionModelAction?.Invoke(model);
            return predictEngine;
        }
        public static PredictionEngine<TIn, TOut> FastTreeTweedie<TIn, TOut>(
            IEnumerable<TIn> trainDataset,
            string exampleWeightColumnName = null,
            int numberOfLeaves = 20,
            int numberOfTrees = 100,
            int minimumExampleCountPerLeaft = 10,
            double learningRate = 0.2,
            Action<ITransformer> additionModelAction = null) where TIn : class, new() where TOut : class, new()
        {
            var context = new MLContext();
            var model = context.RegressionTrainerTemplate(trainDataset, context.Regression.Trainers.FastTreeTweedie(
                    labelColumnName: "Label",
                    featureColumnName: "Features",
                    exampleWeightColumnName: exampleWeightColumnName,
                    numberOfLeaves: numberOfLeaves,
                    numberOfTrees: numberOfTrees,
                    minimumExampleCountPerLeaf: minimumExampleCountPerLeaft,
                    learningRate: learningRate
                    ));

            var predictEngine = context.Model.CreatePredictionEngine<TIn, TOut>(model);
            additionModelAction?.Invoke(model);
            return predictEngine;
        }
        public static PredictionEngine<TIn, TOut> FastTree<TIn, TOut>(
            IEnumerable<TIn> trainDataset,
            string exampleWeightColumnName = null,
            int numberOfLeaves = 20,
            int numberOfTrees = 100,
            int minimumExampleCountPerLeaft = 10,
            double learningRate = 0.2,
            Action<ITransformer> additionModelAction = null) where TIn : class, new() where TOut : class, new()
        {
            var context = new MLContext();
            var model = context.RegressionTrainerTemplate(trainDataset, context.Regression.Trainers.FastTree(
                    labelColumnName: "Label",
                    featureColumnName: "Features",
                    exampleWeightColumnName: exampleWeightColumnName,
                    numberOfLeaves: numberOfLeaves,
                    numberOfTrees: numberOfTrees,
                    minimumExampleCountPerLeaf: minimumExampleCountPerLeaft,
                    learningRate: learningRate
                    ));
            var predictionEngine = context.Model.CreatePredictionEngine<TIn, TOut>(model);
            additionModelAction?.Invoke(model);
            return predictionEngine;
        }
        public static PredictionEngine<TIn, TOut> FastForest<TIn, TOut>(
            IEnumerable<TIn> trainDataset,
            string exampleWeightColumnName = null,
            int numberOfLeaves = 20,
            int numberOfTrees = 100,
            int minimumExampleCountPerLeaft = 10,
            Action<ITransformer> additionModelAction = null) where TIn : class, new() where TOut : class, new()
        {
            var context = new MLContext();
            var model = context.RegressionTrainerTemplate(trainDataset, context.Regression.Trainers.FastForest(
                    labelColumnName: "Label",
                    featureColumnName: "Features",
                    exampleWeightColumnName: exampleWeightColumnName,
                    numberOfLeaves: numberOfLeaves,
                    numberOfTrees: numberOfTrees,
                    minimumExampleCountPerLeaf: minimumExampleCountPerLeaft
                    ));
            var predictionEngine = context.Model.CreatePredictionEngine<TIn, TOut>(model);
            additionModelAction?.Invoke(model);
            return predictionEngine;
        }
    }
}
