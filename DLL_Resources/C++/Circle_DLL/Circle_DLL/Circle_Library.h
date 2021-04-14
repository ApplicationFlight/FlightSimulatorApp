#pragma once

#pragma once

#ifdef CIRCLE_LIBRARY_EXPORTS
#define CIRCLE_LIBRARY_API __declspec(dllexport)
#else
#define CIRCLE_LIBRARY_API __declspec(dllimport)
#endif




extern "C" CIRCLE_LIBRARY_API void circle(const char* CSVreg, const char* CSVanomaly, const char* result);



