

open System
open System.Threading
  type Account = 
      {accountNumber:string; mutable balance:float} 
  
      member this.Withdraw(cash:float) = 
          if cash > this.balance then
              Console.WriteLine("Insufficient Funds. The Amount you wish to withdraw is greater than your current account balance.")
          else
              this.balance <- this.balance - cash
              Console.WriteLine("You have withdrawn £" + cash.ToString() + ". Your balance is now: £" + this.balance.ToString())
  
      member this.Deposit(cash:float) =
          this.balance <- this.balance + cash
          Console.WriteLine("£" + cash.ToString() + " Cash Deposited. Your new Balance is: £" + this.balance.ToString())
  
      member this.Print = 
          Console.WriteLine("Account Number: " + this.accountNumber)
          Console.WriteLine("Balance: £" + this.balance.ToString())
  let CheckAccount balance =
    match  balance with
    |_ when balance<10.0 ->"Balance is low"
    |_ when (balance>=10.0) && (balance<=100.0)->"Balance is OK"
    |_ when balance>100.0->"Balance is high"


  let A1  = {accountNumber="0001"; balance=0.0}
  let A2  = {accountNumber="0002"; balance=51.0}
  let A3  = {accountNumber="0003"; balance=5.0}
  let A4  = {accountNumber="0004"; balance=75.0}
  let A5  = {accountNumber="0005"; balance=50.0}
  let A6  = {accountNumber="0006"; balance=105.0}

  let Accounts=[A1;A2;A3;A4;A5;A6]

  let DisplayAccounts a=
      for a:Account in a do
         a.Print

  




 
  


 type Ticket={seat:int;customer:string}
             member this.Print=
                Console.WriteLine(this.seat)
                Console.WriteLine(this.customer)
 let mutable tickets=[for n in 1..10 ->{Ticket.seat=n; Ticket.customer=""}]

 let DisplayTickets t=
    for t:Ticket in t do
       t.Print

DisplayTickets tickets


let seatNumber = ref 0
let name = ref ""

let bookSeat _ =
    Console.WriteLine("Enter seat number: ")
    seatNumber :=  int(Console.ReadLine())
    Console.WriteLine("Enter customer name: ")
    name:= Console.ReadLine().ToString()
    let book seatNumber name tickets = 
        lock(seatNumber,name) (fun()-> tickets |> List.map (fun ticket ->
            if ticket.seat = seatNumber then { ticket with customer = name }
            else ticket ))    
    tickets <- book !seatNumber !name tickets

ThreadPool.QueueUserWorkItem(new WaitCallback(bookSeat)) |> ignore
ThreadPool.QueueUserWorkItem(new WaitCallback(bookSeat)) |> ignore
Thread.Sleep(5000)


  