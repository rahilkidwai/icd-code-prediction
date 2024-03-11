# Predicting ICD Diagnosis Codes from EHR Records
<center>
    <center>
        <img src=images/med-billing.jpg width=50% />
    </center>
</center>

- [Predicting ICD Diagnosis Codes from EHR Records](#predicting-icd-diagnosis-codes-from-ehr-records)
  - [Problem Statement](#problem-statement)
    - [Background](#background)
  - [Dataset](#dataset)
    - [MIMIC-IV](#mimic-iv)
  - [Data Preprocessing](#data-preprocessing)
  - [Techniques / Evaluation](#techniques--evaluation)
    - [Logistic Regression](#logistic-regression)
      - [Using Count Vectorization](#using-count-vectorization)
      - [Using TFIDF](#using-tfidf)
    - [Naive Bayes](#naive-bayes)
    - [Support Vector Machines](#support-vector-machines)
  - [Conclusion](#conclusion)
  - [References](#references)

## Problem Statement
Automate and recommend diagnosis and procedure codes based on discharge summaries and clinical notes in healthcare domain.
### Background
ICD-10 (International Classification of Diseases, 10th Revision) is a set of codes used to classify diseases, injuries, health encounters, and inpatient procedures published by the World Health Organization (WHO). ICD essentially categorizes diseases and has given each illness a unique code. \
ICD standard is used in every healthcare system and is key component of billing and insurance claims. The ICD-10 version of codes contains more than 70,000 codes. \
Thousands of medical coders do the classification manually by reading patient health records, physician notes and discharge summaries. The complex structure and amount of information makes manual coding challenging, is time consuming and error prone. Billing issues and underpayments can result from coding errors and missing codes. In the last few years lot of efforts have been made to automate this process by leveraging AI and ML.

## Dataset
MIMIC (Medical Information Mart for Intensive Care) [[1]](#1) [[2]](#2) [[3]](#3) is a large, freely-available database comprising de-identified health-related data associated with over forty thousand patients who stayed in critical care units of the Beth Israel Deaconess Medical Center between 2001 and 2012. \
The database includes information such as demographics, vital sign measurements, laboratory test results, procedures, medications, caregiver notes, imaging reports.
### MIMIC-IV
MIMIC IV is the latest release of the dataset. To get access to the dataset, one has to agree to the Data Use Agreement and complete the required training. \
[MIMIC-IV Version 2.2](https://physionet.org/content/mimiciv/2.2/) has been used for this project. \
**Access Policy:** Only credentialed users who sign the DUA can access the files. \
**License (for files):** [PhysioNet Credentialed Health Data License 1.5.0](https://physionet.org/content/mimiciv/view-license/2.2/) \
**Data Use Agreement:** [PhysioNet Credentialed Health Data Use Agreement 1.5.0](https://physionet.org/content/mimiciv/view-dua/2.2/) \
**Required training:** [CITI Data or Specimens Only Research](https://physionet.org/content/mimiciv/view-required-training/2.2/#1)

## Data Preprocessing

## Techniques / Evaluation
This is a Multi Label Classification problem. I plan to explore various techniques including traditional Machine Learning models like Logistic Regression, State Vector Machines (SVM), TV_IDF, Naive Bayes, K-Nearest Neighbors (KNN).
Also would like to explore the natural language processing (NLP) technique (word embedding, Word2Vector) for processing data and a deep learning-based Convolutional Neural Network (CNN) model.
To compare different techniques I plan to use the following metrics: Precision, Recall, and F-score.
### Logistic Regression
#### Using Count Vectorization
#### Using TFIDF
### Naive Bayes
### Support Vector Machines
## Conclusion

## References
<a id="1">[1]</a> Johnson, A., Bulgarelli, L., Pollard, T., Horng, S., Celi, L. A., & Mark, R. (2023). MIMIC-IV (version 2.2). PhysioNet. https://doi.org/10.13026/6mm1-ek67. \
<a id="2">[2]</a> Johnson, A.E.W., Bulgarelli, L., Shen, L. et al. MIMIC-IV, a freely accessible electronic health record dataset. Sci Data 10, 1 (2023). https://doi.org/10.1038/s41597-022-01899-x \
<a id="3">[3]</a> Goldberger, A., Amaral, L., Glass, L., Hausdorff, J., Ivanov, P. C., Mark, R., ... & Stanley, H. E. (2000). PhysioBank, PhysioToolkit, and PhysioNet: Components of a new research resource for complex physiologic signals. Circulation [Online]. 101 (23), pp. e215–e220. \