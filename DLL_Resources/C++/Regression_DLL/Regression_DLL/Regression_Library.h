#pragma once

#ifdef REGRESSION_LIBRARY_EXPORTS
#define REGRESSION_LIBRARY_API __declspec(dllexport)
#else
#define REGRESSION_LIBRARY_API __declspec(dllimport)
#endif




extern "C" REGRESSION_LIBRARY_API void regression(const char* CSVreg, const char* CSVanomaly, const char* result);


