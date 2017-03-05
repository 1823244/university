#include "stdafx.h"
#include "lab1.h"

int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "RUS");

	HCRYPTPROV hProv;

	if (! CryptAcquireContext(&hProv, NULL, NULL, PROV_RSA_AES, CRYPT_VERIFYCONTEXT))
	{
		_tprintf(_T("Ошибка подключения к криптопровайдеру.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	} else {
		_tprintf(_T("Криптопровайдер подключён успешно.\n\n"));
	}
	
	UINT crypt;
	_tprintf(_T("Зашифровать или расшифровать?\n"));
	_tprintf(_T("1. Зашифровать\n"));
	_tprintf(_T("2. Расшифровать\n"));
	_tprintf(_T("[<1>/2]: "));
	_tscanf(_T("%u"), &crypt);

	if (crypt < 1 || crypt > 2) {
		crypt = 1;
	}

	TCHAR input[MAX_STRING_LENGTH], output[MAX_STRING_LENGTH];

	if (crypt == 1) {
		_tprintf(_T("Введите название файла, который нужно зашифровать: "));
		_tscanf(_T("%s"), input);

		_tprintf(_T("Введите название для зашифрованного файла: "));
		_tscanf(_T("%s"), output);
	} else {
		_tprintf(_T("Введите название файла, который нужно расшифровать: "));
		_tscanf(_T("%s"), input);

		_tprintf(_T("Введите название для расшифрованного файла: "));
		_tscanf(_T("%s"), output);
	}

	TCHAR password[MAX_STRING_LENGTH];
	_tprintf(_T("Введите пароль: "));
	_tscanf(_T("%s"), password);

	UINT mode;
	_tprintf(_T("Выберите режим шифрования:\n"));
	_tprintf(_T("1. AES-128\n"));
	_tprintf(_T("2. AES-192\n"));
	_tprintf(_T("3. AES-256\n"));
	_tprintf(_T("[<1>/2/3]: "));
	_tscanf(_T("%u"), &mode);

	if (mode < 1 || mode > 3) {
		mode = 1;
	}

	UINT crypttype;
	_tprintf(_T("Выберите криптографический интерфейс:\n"));
	_tprintf(_T("1. CryptoAPI\n"));
	_tprintf(_T("2. Cryptography API: Next Generation\n"));
	_tprintf(_T("[<1>/2]: "));
	_tscanf(_T("%u"), &crypttype);

	if (crypttype < 1 || crypttype > 2) {
		crypttype = 1;
	}

	UINT blockmode;
	if (crypttype == 2) {
		_tprintf(_T("Выберите режим блочного шифрования:\n"));
		_tprintf(_T("1. ECB\n"));
		_tprintf(_T("2. CBC\n"));
		_tprintf(_T("3. CFB\n"));
		_tprintf(_T("[<1>/2/3]: "));
		_tscanf(_T("%u"), &blockmode);

		if (blockmode < 1 || blockmode > 3) {
			blockmode = 1;
		}
	}

	if (crypt == 1) {
		if (crypttype == 1) {
			encrypt(hProv, input, output, password, mode);
		} else {
			encryptng(hProv, input, output, password, mode, blockmode);
		}
	} else {
		if (crypttype == 1) {
			decrypt(hProv, output, input, password, mode);
		} else {
			decryptng(hProv, output, input, password, mode, blockmode);
		}
	}

	CryptReleaseContext(hProv, 0);

	return 0;
}

void encrypt(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode) {
	HCRYPTHASH hHash;

	BOOL createdHash = CryptCreateHash(provider, CALG_SHA_256, 0, 0, &hHash);

	if (! createdHash) {
		_tprintf(_T("Ошибка при создании хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	createdHash = CryptHashData(hHash, (PBYTE)password, _tcslen(password)*sizeof(TCHAR), 0);

	if (! createdHash) {
		_tprintf(_T("Ошибка при генерации хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	ALG_ID AESType;

	switch (mode) {
		case 1: AESType = CALG_AES_128;
			break;
		case 2: AESType = CALG_AES_192;
			break;
		case 3: AESType = CALG_AES_256;
			break;
	}

	HCRYPTKEY hKeyH;

	createdHash = CryptDeriveKey(provider, AESType, hHash, 0, &hKeyH);

	if (! createdHash) {
		_tprintf(_T("Ошибка создании сессионного ключа.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	CryptDestroyHash(hHash);

	HANDLE input = CreateFile(inputPath, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (input == NULL) {
		_tprintf(_T("Ошибка при открытии файла с текстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	HANDLE output = CreateFile(outputPath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, 0, NULL);

	if (output == NULL) {
		_tprintf(_T("Ошибка при создании файла для шифртекста.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	unsigned long read, written;
	const unsigned int length = 496;
	BOOL eof = false;

	do {
		byte s[length];

		eof = ! ReadFile(input, s, length, &read, NULL);
		if (eof || read == 0) break;

		bool final = read != length;

		BOOL crypted = CryptEncrypt(hKeyH, 0, final, 0, s, &read, read + 16);

		if (! crypted && ! final) {
			_tprintf(_T("Ошибка при зашифровывании файла.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);		
		}

		WriteFile(output, s, read, &written, NULL);

	} while (true);

	CloseHandle(input);
	CloseHandle(output);

	CryptDestroyHash(hKeyH);

	_tprintf(_T("Файл зашифрован успешно.\n"));
	system("PAUSE");
}

void decrypt(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode) {
	HCRYPTHASH hHash;

	BOOL createdHash = CryptCreateHash(provider, CALG_SHA_256, 0, 0, &hHash);

	if (! createdHash) {
		_tprintf(_T("Ошибка при создании хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	createdHash = CryptHashData(hHash, (PBYTE)password, _tcslen(password)*sizeof(TCHAR), 0);

	if (! createdHash) {
		_tprintf(_T("Ошибка при генерации хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	ALG_ID AESType;

	switch (mode) {
		case 1: AESType = CALG_AES_128;
			break;
		case 2: AESType = CALG_AES_192;
			break;
		case 3: AESType = CALG_AES_256;
			break;
	}

	HCRYPTKEY hKeyH;

	createdHash = CryptDeriveKey(provider, AESType, hHash, 0, &hKeyH);

	if (! createdHash) {
		_tprintf(_T("Ошибка при создании сессионного ключа.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	CryptDestroyHash(hHash);

	HANDLE input = CreateFile(inputPath, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (input == NULL) {
		_tprintf(_T("Ошибка при открытии файла с шифртекстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	HANDLE output = CreateFile(outputPath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, 0, NULL);

	if (output == NULL) {
		_tprintf(_T("Ошибка при создании файла с исходным текстом.\n"));
		exit(EXIT_FAILURE);		
	}

	unsigned long read, written;
	const unsigned int length = 496;
	BOOL eof = false;

	do {
		byte s[length];

		eof = ! ReadFile(input, s, length, &read, NULL);
		if (eof || read == 0) break;

		bool final = read != length;

		BOOL crypted = CryptDecrypt(hKeyH, 0, final, 0, s, &read);

		if (! crypted && ! final) {
			_tprintf(_T("Ошибка при расшифровывании файла.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);		
		}

		WriteFile(output, s, read, &written, NULL);

	} while (true);

	CloseHandle(input);
	CloseHandle(output);

	CryptDestroyHash(hKeyH);

	_tprintf(_T("Файл успешно расшифрован.\n"));
	system("PAUSE");
}

void encryptng(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode, UINT blockmode) {
	NTSTATUS status = STATUS_UNSUCCESSFUL;

	BCRYPT_ALG_HANDLE phAlgorithmSHA256;
	if (! BCRYPT_SUCCESS(status = BCryptOpenAlgorithmProvider(&phAlgorithmSHA256, BCRYPT_SHA256_ALGORITHM, NULL, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма SHA-256.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	DWORD cbObject = 0, cbSHA256ObjectLength = 0;
	PBYTE pbSHA256Object = NULL;
	BCryptGetProperty(phAlgorithmSHA256, BCRYPT_OBJECT_LENGTH, (PBYTE)&cbSHA256ObjectLength, sizeof(DWORD), &cbObject, 0);
	pbSHA256Object = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbSHA256ObjectLength);

	BCRYPT_HASH_HANDLE phHashSHA256;
	if (! BCRYPT_SUCCESS(status = BCryptCreateHash(phAlgorithmSHA256, &phHashSHA256, pbSHA256Object, cbSHA256ObjectLength, NULL, 0, 0))) {
		_tprintf(_T("Ошибка при создании хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	if (! BCRYPT_SUCCESS(status = BCryptHashData(phHashSHA256, (PBYTE)password, _tcslen(password) * sizeof(TCHAR), 0))) {
		_tprintf(_T("Ошибка при хэшировании пароля.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	PBYTE pbHash = NULL;
	DWORD cbData = 0, cbHash = 0;
	BCryptGetProperty(phAlgorithmSHA256, BCRYPT_HASH_LENGTH, (PBYTE)&cbHash, sizeof(DWORD), &cbData, 0);
	pbHash = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbHash);	
	
	if (! BCRYPT_SUCCESS(status = BCryptFinishHash(phHashSHA256, pbHash, cbHash, 0))) {
		_tprintf(_T("Ошибка при извлечении хэш-кода из хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	BCRYPT_ALG_HANDLE phAlgorithmAES;
	if (! BCRYPT_SUCCESS(status = BCryptOpenAlgorithmProvider(&phAlgorithmAES, BCRYPT_AES_ALGORITHM, NULL, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма AES.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	switch (blockmode) {
		case 1: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_ECB, sizeof(BCRYPT_CHAIN_MODE_ECB), 0);
			break;
		case 2: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_CBC, sizeof(BCRYPT_CHAIN_MODE_CBC), 0);
			break;
		case 3: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_CFB, sizeof(BCRYPT_CHAIN_MODE_CFB), 0);
			break;
	}

	DWORD cbAESLength = 0;
	PBYTE pbAESObject = NULL;
	BCryptGetProperty(phAlgorithmAES, BCRYPT_OBJECT_LENGTH, (PBYTE)&cbAESLength, sizeof(DWORD), &cbObject, 0);
	pbAESObject = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbAESLength);

	ULONG AESType;

	switch (mode) {
		case 1: AESType = 16;
			break;
		case 2: AESType = 24;
			break;
		case 3: AESType = 32;
			break;
	}

	BCRYPT_KEY_HANDLE phKey;
	if (! BCRYPT_SUCCESS(status = BCryptGenerateSymmetricKey(phAlgorithmAES, &phKey, pbAESObject, cbAESLength, (PBYTE)pbHash, AESType, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма AES.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	HANDLE input = CreateFile(inputPath, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (input == NULL) {
		_tprintf(_T("Ошибка при открытии файла с исходным текстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	HANDLE output = CreateFile(outputPath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, 0, NULL);

	if (output == NULL) {
		_tprintf(_T("Ошибка при создании файла с шифртекстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	unsigned long read, written;
	const unsigned int length = 496;
	bool eof = false;

	do {
		byte s[length];

		eof = ! ReadFile(input, s, length, &read, NULL);
		if (eof || read == 0) break;

		ULONG dwFlags = 0;

		if (read != length) {
			dwFlags = BCRYPT_BLOCK_PADDING;
		}

		DWORD cbOutput = 0, cbCipherText = 0;
		PBYTE pbOutput = NULL;
		if (! BCRYPT_SUCCESS(status = BCryptEncrypt(phKey, s, read, NULL, NULL, 0, NULL, 0, &cbOutput, dwFlags)) && read == length) {
			_tprintf(_T("Ошибка при вычислении размера шифркода.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);
		}

		pbOutput = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbOutput);

		if (! BCRYPT_SUCCESS(status = BCryptEncrypt(phKey, s, read, NULL, NULL, 0, pbOutput, cbOutput, &cbCipherText, dwFlags)) && read == length) {
			_tprintf(_T("Ошибка при зашифровывании файла.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);
		}

		WriteFile(output, pbOutput, cbCipherText, &written, NULL);

		HeapFree(GetProcessHeap(), 0, pbOutput);

	} while (true);

	CloseHandle(input);
	CloseHandle(output);

	BCryptDestroyHash(phHashSHA256);
	BCryptDestroyKey(phKey);
	BCryptCloseAlgorithmProvider(phAlgorithmAES, 0);
	BCryptCloseAlgorithmProvider(phAlgorithmSHA256, 0);

	HeapFree(GetProcessHeap(), 0, pbSHA256Object);
	HeapFree(GetProcessHeap(), 0, pbAESObject);
	HeapFree(GetProcessHeap(), 0, pbHash);

	_tprintf(_T("Файл успешно зашифрован.\n"));
	system("PAUSE");
}

void decryptng(HCRYPTPROV provider, TCHAR *inputPath, TCHAR *outputPath, TCHAR *password, UINT mode, UINT blockmode) {
	NTSTATUS status = STATUS_UNSUCCESSFUL;

	BCRYPT_ALG_HANDLE phAlgorithmSHA256;
	if (! BCRYPT_SUCCESS(status = BCryptOpenAlgorithmProvider(&phAlgorithmSHA256, BCRYPT_SHA256_ALGORITHM, NULL, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма SHA-256.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	DWORD cbObject = 0, cbSHA256ObjectLength = 0;
	PBYTE pbSHA256Object = NULL;
	BCryptGetProperty(phAlgorithmSHA256, BCRYPT_OBJECT_LENGTH, (PBYTE)&cbSHA256ObjectLength, sizeof(DWORD), &cbObject, 0);
	pbSHA256Object = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbSHA256ObjectLength);

	BCRYPT_HASH_HANDLE phHashSHA256;
	if (! BCRYPT_SUCCESS(status = BCryptCreateHash(phAlgorithmSHA256, &phHashSHA256, pbSHA256Object, cbSHA256ObjectLength, NULL, 0, 0))) {
		_tprintf(_T("Ошибка при создании хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	if (! BCRYPT_SUCCESS(status = BCryptHashData(phHashSHA256, (PBYTE)password, _tcslen(password) * sizeof(TCHAR), 0))) {
		_tprintf(_T("Ошибка при хэшировании пароля.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	PBYTE pbHash = NULL;
	DWORD cbData = 0, cbHash = 0;
	BCryptGetProperty(phAlgorithmSHA256, BCRYPT_HASH_LENGTH, (PBYTE)&cbHash, sizeof(DWORD), &cbData, 0);
	pbHash = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbHash);	
	
	if (! BCRYPT_SUCCESS(status = BCryptFinishHash(phHashSHA256, pbHash, cbHash, 0))) {
		_tprintf(_T("Ошибка при извлечении хэш-кода из хэш-объекта.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	BCRYPT_ALG_HANDLE phAlgorithmAES;
	if (! BCRYPT_SUCCESS(status = BCryptOpenAlgorithmProvider(&phAlgorithmAES, BCRYPT_AES_ALGORITHM, NULL, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма AES.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	switch (blockmode) {
		case 1: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_ECB, sizeof(BCRYPT_CHAIN_MODE_ECB), 0);
			break;
		case 2: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_CBC, sizeof(BCRYPT_CHAIN_MODE_CBC), 0);
			break;
		case 3: BCryptSetProperty(phAlgorithmAES, BCRYPT_CHAINING_MODE, (PBYTE)BCRYPT_CHAIN_MODE_CFB, sizeof(BCRYPT_CHAIN_MODE_CFB), 0);
			break;
	}

	DWORD cbAESLength = 0;
	PBYTE pbAESObject = NULL;
	BCryptGetProperty(phAlgorithmAES, BCRYPT_OBJECT_LENGTH, (PBYTE)&cbAESLength, sizeof(DWORD), &cbObject, 0);
	pbAESObject = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbAESLength);

	ULONG AESType;

	switch (mode) {
		case 1: AESType = 16;
			break;
		case 2: AESType = 24;
			break;
		case 3: AESType = 32;
			break;
	}

	BCRYPT_KEY_HANDLE phKey;
	if (! BCRYPT_SUCCESS(status = BCryptGenerateSymmetricKey(phAlgorithmAES, &phKey, pbAESObject, cbAESLength, (PBYTE)pbHash, AESType, 0))) {
		_tprintf(_T("Ошибка при получении дескриптера алгоритма AES.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);
	}

	HANDLE input = CreateFile(inputPath, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (input == NULL) {
		_tprintf(_T("Ошибка при открытии файла с шифртекстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	HANDLE output = CreateFile(outputPath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, 0, NULL);

	if (output == NULL) {
		_tprintf(_T("Ошибка при создании файла с исходным текстом.\n"));
		system("PAUSE");
		exit(EXIT_FAILURE);		
	}

	unsigned long read, written;
	const unsigned int length = 496;
	bool eof = false;

	do {
		byte s[length];

		eof = ! ReadFile(input, s, length, &read, NULL);
		if (eof || read == 0) break;

		ULONG dwFlags = 0;

		if (read != length) {
			dwFlags = BCRYPT_BLOCK_PADDING;
		}

		DWORD cbOutput = 0, cbCipherText = 0;
		PBYTE pbOutput = NULL;
		if (! BCRYPT_SUCCESS(status = BCryptDecrypt(phKey, s, read, NULL, NULL, 0, NULL, 0, &cbCipherText, dwFlags)) && read == length) {
			_tprintf(_T("Ошибка при вычислении размера исходного текста.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);
		}

		pbOutput = (PBYTE)HeapAlloc(GetProcessHeap(), 0, cbCipherText);

		if (! BCRYPT_SUCCESS(status = BCryptDecrypt(phKey, s, read, NULL, NULL, 0, pbOutput, cbCipherText, &cbOutput, dwFlags)) && read == length) {
			_tprintf(_T("Ошибка при расшифрованиии файла с шифртекстом.\n"));
			system("PAUSE");
			exit(EXIT_FAILURE);
		}

		WriteFile(output, pbOutput, cbOutput, &written, NULL);

		HeapFree(GetProcessHeap(), 0, pbOutput);

	} while (true);

	CloseHandle(input);
	CloseHandle(output);

	BCryptDestroyHash(phHashSHA256);
	BCryptDestroyKey(phKey);
	BCryptCloseAlgorithmProvider(phAlgorithmAES, 0);
	BCryptCloseAlgorithmProvider(phAlgorithmSHA256, 0);

	HeapFree(GetProcessHeap(), 0, pbSHA256Object);
	HeapFree(GetProcessHeap(), 0, pbAESObject);
	HeapFree(GetProcessHeap(), 0, pbHash);

	_tprintf(_T("Файл успешно расшифрован.\n"));
	system("PAUSE");
}