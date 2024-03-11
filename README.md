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
    - [Data Structure](#data-structure)
    - [Data Loading](#data-loading)
      - [Data Format](#data-format)
      - [Data Exploration](#data-exploration)
  - [Data Preprocessing](#data-preprocessing)
    - [Study Constraints](#study-constraints)
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
### Data Structure
MIMIC-IV dataset comprises of different modules as described below: \
**Hosp** module contains all data acquired from the hospital wide electronic health record. Information covered includes patient and admission information, laboratory measurements, microbiology, medication administration, and billed diagnoses. \
**ICU** module contains information collected from the clinical information system used within the ICU and includes intravenous administrations, ventilator settings, and other charted items. \
**ED** module contains data for emergency department patients and includes reason for admission, triage assessment, vital signs, and medicine reconciliaton. \
**CXR** module contains patient chest x-rays and can linked with the clinical data from other modules. \
**ECG** module contains waveform data, and lookup tables which can be used to link subjects to other modules. \ 
**Note** module contains de-identified free-text clinical notes for patients.
### Data Loading
<pre>
Database: PostgreSql
</pre>
#### Data Format
The data is available to download as csv files for multiple relational databases. For the purpose of this study PostgreSql is the chosen relational database engine. \
Once the files are downloaded, I followed the steps described [here](https://github.com/MIT-LCP/mimic-code/blob/main/mimic-iv/buildmimic/postgres/README.md) to load data into postgreSql instance.
#### Data Exploration
<pre>
Database Schemas: mimiciv_derived, mimiciv_ed, mimiciv_hosp, mimiciv_icu, mimiciv_note
</pre>
<pre>
Schemas related to study:<br/>mimiciv_hosp,<br/>mimiciv_note
</pre>
MIMIC-IV data is distributed across 5 different schemas. All the data needed for this study is confined in "hosp" and "note" schemas.
<pre>
Tables related to study:<br/>mimiciv_hosp.d_icd_diagnoses,<br/>mimiciv_hosp.icd_diagnoses,<br/>mimiciv_note.discharge
</pre>
**mimiciv_hosp.d_icd_diagnoses** It is a dictionary table and contains all icd codes and their description. The dictionary contains both ICD9 and ICD10 codes. ICD9 is the older version and has been replaced by ICD10 almost a decade ago. Most EHR systems still has ICD9 to cater for historical data. For the purpose of this study we are only focus on ICD10. \
**mimiciv_note.discharge** This table contains the discharge notes of patients for specific hospitalization. \
**mimiciv_hosp.icd_diagnoses** This table contain all the diagnosed icd codes for patient tied to their visit / hospitalization.

## Data Preprocessing
ICD Code Dictionary Stats:
|   |   |
|---|---|
| Total ICD Codes | 109,775 |
| Total ICD 10 Codes | 95,109 |
| Total ICD 10 Codes (Diabetes) | 642 |

### Study Constraints
There are a total of ~110K ICD Codes (including ICD9), whereas ~95K of the total are ICD10 codes. \
The ultimate goal is to predict any of the ICD10 code based on clinical documents but for this study we are going to limit prediction to a subset of codes and are going to limit the data to predict codes only related to Diabetes. \
There are a total of 642 different ICD10 codes in the dictionary related to diabetes which can be categorized into 13 different categories: E08,E09,E10,E11,E13,E23,N25,O24,P70,R73,Z13,Z83,Z86

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