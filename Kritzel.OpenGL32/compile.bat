g++ -DGLEW_STATIC nativegl.cpp -o Kritzel.OpenGl32.dll -shared -Wall -O2 -I./include -L./lib -lglfw3 -lglew32 -lopengl32 -lkernel32 -luser32 -lgdi32 -static
copy Kritzel.OpenGl32.dll ..\Kritzel.GL\Kritzel.OpenGl32.dll /Y