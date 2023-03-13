var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Week 3 Assignments");

//Temperature 
app.MapGet("/temperature", (decimal input) =>
{
  Temperature temperature1 = new Temperature(input);  
  return ($"{temperature1.Celsius} Celsius is {temperature1.Fahrenheit} Fahrenheit");
});

//Exchange
app.MapGet("/exchange", (decimal input) =>
{
  var amount = input;
  var exchangeRate = new ExchangeRate("EUR", "DKK");
  exchangeRate.Rate = 7.5m;
  return ($"{amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}");
});

//Account
app.MapGet("/account", (decimal amount, string method) =>
{
  var account = new Account(100m);
   
  if(method == "deposit")
  {
    account.Deposit(amount);
  }
  else if (method == "withdraw")
  {
    account.Withdraw(amount);
  }
  else
  {
    return ("method should be deposit or withdraw");
  }
  return ($"Account balance is {account.Balance}");
  
});

//Animal Interface
app.MapGet("/animal", () =>
{
  Cow cow = new Cow();
  Cat cat = new Cat();
  Dog dog = new Dog();
  Sound sound = new Sound();

  var result1 = sound.MakeSound(cow);
  var result2 = sound.MakeSound(cat);
  var result3 = sound.MakeSound(dog);

  return ($"{result1}, {result2}, and {result3}");
});

app.Run();


/* 
Create a class Named Temperature and make a constructor with one decimal 
parameter - degrees (Celsius) and assign its value to Property. The property 
has a public getter and private setter. The property setter throws an 
exception if temperature is less than 273.15 Celsius. Then, create a property 
Fahrenheit that will convert Celsius to Fahrenheit (it has no setter similar to NicePrintout) */

public class Temperature
{
  private decimal _celsius;
  public Temperature(decimal degrees)
  {
    Celsius = degrees;
  }
  public decimal Celsius 
  { 
    get { return _celsius; }
    
    private set
    {
     if (value < 273.15m)
        {
          throw new ArgumentException("Temperature must be greater than 273.15 Celsius.");
        }
      _celsius = value;
    }
  }
  public decimal Fahrenheit => Celsius * 9 / 5 + 32;
     
} 

/* Create a class Named ExchangeRate with a constructor with two string parameters, fromCurrency and toCurrency. 
Add a decimal property called Rate and method Calculate with decimal parameter amount return value of the 
method should be a product of Rate and amount multiplication. */

public class ExchangeRate
{
    public string FromCurrency;
    public string ToCurrency;
    private decimal rate;

    public ExchangeRate(string fromCurrency, string toCurrency)
    {
        FromCurrency = fromCurrency;
        ToCurrency = toCurrency;
        rate = 1.0m;
    }

    public decimal Rate
    {
        get { return rate; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Exchange rate cannot be negative.");
            }
            rate = value;
        }
    }

    public decimal Calculate(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
        return rate * amount;
    }
}

/* Create Account class that can be initialized with the amount value. Account class contains 
Withdraw and Deposit methods and Balance (get only) property. Make sure that you 
can't withdraw more than you have in the balance. */

public class Account
{
    private decimal _balance;

    public Account(decimal Balance)
    {
        _balance = Balance;
    }

    public decimal Deposit(decimal amount)
    {
        return _balance += amount;
    }

    public decimal Withdraw(decimal amount)
    {
        if (amount > _balance)
        {
            throw new InvalidOperationException("Insufficient balance");
        }

       return _balance -= amount;
    }

    public decimal Balance
    {
      get { return _balance; }
    }
}

/* Create interface IAnimal with property Name and Sound. Create classes Cow, Cat and Dog that implement IAnimal. 
Instantiate all three classes and pass them to a new method called MakeSound that has single parameter IAnimal 
and it writes to console eg “Dog says woof woof” if instance of the Dog class is passed. */

interface IAnimal
{
  string Name { get; }
  string Sound {get; }
}

public class Cow : IAnimal
{
  public  string Name { get; } = "Cow" ;
  public  string Sound {get; } = "Moo" ;
}

public class Cat : IAnimal
{
  public  string Name { get; } = "Cat" ;
  public  string Sound {get; set;} = "Meow" ;
}

public class Dog : IAnimal
{
  public  string Name { get; } = "Dog" ;
  public  string Sound {get; } = "Woof" ;
}

class Sound{
  public string MakeSound(IAnimal animal)
  {
    return ($"{animal.Name} says {animal.Sound} {animal.Sound}");
  }

}
