#include "pch.h"
#include <utility>
#include <limits.h>

#include "Regression_Library.h"

#include "anomaly_detection_util.h"
#include "SimpleAnomalyDetector.h"
#include <vector>
#include <algorithm>
#include <string.h>
#include <math.h>


void regression(const char* CSVreg, const char* CSVanomaly,const char* result) {
    TimeSeries ts(CSVreg);
    SimpleAnomalyDetector ad;
    ad.learnNormal(ts);
    vector<correlatedFeatures> cf = ad.getNormalModel();
    TimeSeries ts2(CSVanomaly);
    vector<AnomalyReport> r = ad.detect(ts2);
    fstream my_file;
    my_file.open(result, ios::out);
    if (my_file) {
        for (int i = 0; i < r.size(); i++) {
            my_file << r[i].features + ',' + std::to_string(r[i].timeStep) + ',' + r[i].description + "\n";
        }
        my_file.close();
    }
}
