// See https://aka.ms/new-console-template for more information
using Engine;

Console.WriteLine("Hello, World!");

ulong mask = 1;
for (int i = 0; i < Board.Size; i++)
{
    Console.WriteLine(mask);// BitConverter.ToUInt64(mask));
    mask <<= 1;
}