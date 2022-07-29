namespace State; //Vəziyyət


public abstract class State
{
    protected Account account;
    protected double balance;
    protected double interest;
    protected double lowerLimit;
    protected double upperLimit;
    // Xüsusiyyətlər
    public Account Account
    {
        get { return account; }
        set { account = value; }
    }
    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }
    public abstract void Deposit(double amount);
    public abstract void Withdraw(double amount);
    public abstract void PayInterest();
}

// RedState hesabın artıq olduğunu göstərir 
public class RedState : State
{
    private double serviceFee;
    // Constructor
    public RedState(State state)
    {
        this.balance = state.Balance;
        this.account = state.Account;
        Initialize();
    }
    private void Initialize()
    {
        // Məlumatlar datasource-dan gəlməlidir
        interest = 0.0;
        lowerLimit = -100.0;
        upperLimit = 0.0;
        serviceFee = 15.00;
    }
    public override void Deposit(double amount)
    {
        balance += amount;
        StateChangeCheck();
    }
    public override void Withdraw(double amount)
    {
        amount = amount - serviceFee;
        Console.WriteLine("No funds available for withdrawal!");
    }
    public override void PayInterest()
    {
        // Faiz ödənilmir
    }
    private void StateChangeCheck()
    {
        if (balance > upperLimit)
        {
            account.State = new SilverState(this);
        }
    }
}

// SilverState faizsiz olduğunu göstərir
public class SilverState : State
{
    // Overloaded constructors
    public SilverState(State state) :
        this(state.Balance, state.Account)
    {
    }
    public SilverState(double balance, Account account)
    {
        this.balance = balance;
        this.account = account;
        Initialize();
    }
    private void Initialize()
    {
        // Məlumatlar datasource-dan gəlməlidir
        interest = 0.0;
        lowerLimit = 0.0;
        upperLimit = 1000.0;
    }
    public override void Deposit(double amount)
    {
        balance += amount;
        StateChangeCheck();
    }
    public override void Withdraw(double amount)
    {
        balance -= amount;
        StateChangeCheck();
    }
    public override void PayInterest()
    {
        balance += interest * balance;
        StateChangeCheck();
    }
    private void StateChangeCheck()
    {
        if (balance < lowerLimit)
        {
            account.State = new RedState(this);
        }
        else if (balance > upperLimit)
        {
            account.State = new GoldState(this);
        }
    }
}

// GoldState faiz dərəcəsini göstərir

public class GoldState : State
{
    // Overloaded constructors
    public GoldState(State state)
        : this(state.Balance, state.Account)
    {
    }
    public GoldState(double balance, Account account)
    {
        this.balance = balance;
        this.account = account;
        Initialize();
    }
    private void Initialize()
    {
        // Məlumatlar database-dan gəlməlidir
        interest = 0.05;
        lowerLimit = 1000.0;
        upperLimit = 10000000.0;
    }
    public override void Deposit(double amount)
    {
        balance += amount;
        StateChangeCheck();
    }
    public override void Withdraw(double amount)
    {
        balance -= amount;
        StateChangeCheck();
    }
    public override void PayInterest()
    {
        balance += interest * balance;
        StateChangeCheck();
    }
    private void StateChangeCheck()
    {
        if (balance < 0.0)
        {
            account.State = new RedState(this);
        }
        else if (balance < lowerLimit)
        {
            account.State = new SilverState(this);
        }
    }
}

public class Account
{
    private State state;
    private string owner;
    // Constructor
    public Account(string owner)
    {
        // Yeni akauntlar standart olaraq'Silver'-dir
        this.owner = owner;
        this.state = new SilverState(0.0, this);
    }
    public double Balance
    {
        get { return state.Balance; }
    }
    public State State
    {
        get { return state; }
        set { state = value; }
    }
    public void Deposit(double amount)
    {
        state.Deposit(amount);
        Console.WriteLine("Deposited {0:C} --- ", amount);
        Console.WriteLine(" Balance = {0:C}", this.Balance);
        Console.WriteLine(" Status  = {0}",
            this.State.GetType().Name);
        Console.WriteLine("");
    }
    public void Withdraw(double amount)
    {
        state.Withdraw(amount);
        Console.WriteLine("Withdrew {0:C} --- ", amount);
        Console.WriteLine(" Balance = {0:C}", this.Balance);
        Console.WriteLine(" Status  = {0}\n",
            this.State.GetType().Name);
    }
    public void PayInterest()
    {
        state.PayInterest();
        Console.WriteLine("Interest Paid --- ");
        Console.WriteLine(" Balance = {0:C}", this.Balance);
        Console.WriteLine(" Status  = {0}\n",
            this.State.GetType().Name);
    }
}