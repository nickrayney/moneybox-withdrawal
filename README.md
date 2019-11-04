# Moneybox Money Withdrawal

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. 
For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, 
and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, 
however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. 
This should simplify the task of implementing WithdrawMoney.Execute(...).

## Guidelines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work 
(e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed your work, send us a link to your public repository.

Good luck!

## Response
I added the checks that were originally made in the TransferMoney class into the Account domain class. I did this in the form of public booleans that show whether or 
not an operation is possible for that account. I also made figures like the balance, paid-in amount or withdrawn amount read-only by changing them to private fields with 
a public get accessor, in order to protect these fields by potentially being altered by feature classes outside the domain class. The only one can update the balance etc. is 
by using the methods in the domain class which will accept or withdraw funds. 

I also moved some of the values of the variables of the account, such as the daily pay-in amount or the withdrawn amount, from the feature class to the domain class because those 
values are features of the account and shouldn't be defined in the feature class. Although they are a defined value in the account class, we may in future get this information 
from the repository depending on the type of account the user has - so this could be something that changes in future. 

Once I refactored the TransferMoney class, it was easy to reuse the new methods from the Account class in the WithdrawMoney class. 

I then also wrote a small number of unit tests to make sure the methods in the Account class are behaving correctly and added this to the Moneybox.Refactor solution as a new project.
I used the MSTest framework for this to just create some simple tests to check the operations I wrote. 


