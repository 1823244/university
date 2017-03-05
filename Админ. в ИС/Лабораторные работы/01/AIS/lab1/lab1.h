#include <atlstr.h>
#include <Windows.h>
#include <bcrypt.h>
#include <WinCrypt.h>
#include <locale.h>
#include <fcntl.h>
#include <io.h>
#include <stdio.h>

#ifndef STATUS_SUCCESS
#define STATUS_SUCCESS                  ((NTSTATUS)0x00000000L)
#define STATUS_UNSUCCESSFUL             ((NTSTATUS)0xC0000001L)
#endif

#define MAX_STRING_LENGTH 255

void encrypt(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode);
void decrypt(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode);
void encryptng(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode, UINT blockmode);
void decryptng(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode, UINT blockmode);