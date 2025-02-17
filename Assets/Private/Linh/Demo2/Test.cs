using System;

abstract class Animal
{
    public abstract void Speak(); 
    public void Sleep()
    {
        Console.WriteLine("Con v?t ?ang ng?...");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Chó s?a: Gâu gâu!");
    }
}

class Program
{
    static void Main()
    {
        Dog myDog = new Dog();
        myDog.Speak(); 
    }
}