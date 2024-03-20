// game_engine_detector.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include <cstdlib>
#include <windows.h>

using std::cout;
using std::wcout;
using std::endl;

typedef const char* (*DETECT_FUNC)(const wchar_t*, const wchar_t*);
typedef void (*FREE_FUNC)(const char*);

int main(int argc, wchar_t* argv[])
{
	wchar_t* dllPath;
	if (argc != 3 && argc != 4)
	{
		wcout << "Usage: game_engine_detector <path_to_exe> <path_to_base_dir> (<path_to_dll>)" << endl;
		return -1;
	}
	if (argc == 4)
	{
		dllPath = argv[3];
	}
	else
	{
		wchar_t t[] = L"lib_game_engine_detector.dll";
		dllPath = t;
	}
	HMODULE dll = LoadLibrary(dllPath);
	if (dll == NULL)
	{
		wcout << "Failed to load " << dllPath << endl;
		return -1;
	}
	DETECT_FUNC Detect = (DETECT_FUNC)GetProcAddress(dll, "Detect");
	if (Detect == NULL)
	{
		wcout << "Failed to get detect function 'Detect'" << endl;
		return -1;
	}
	FREE_FUNC FreeString = (FREE_FUNC)GetProcAddress(dll, "FreeString");
	if (FreeString == NULL)
	{
		wcout << "Failed to get free function 'FreeString'" << endl;
		return -1;
	}

	const char* result = Detect(argv[1], argv[2]);

	cout << result << endl;

	FreeString(result);
	FreeLibrary(dll);

	return 0;
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
