# Library
This repository provides code for a library system, which can manage the books, members and borrow transactions.

System requirements
Visual studio 2022, SQL Server management studio, postman.
Required:
Dotnet 6.x SDK
Dotnet 6.x Runtime

How to test the APIs
Set the Services project as startup project, 
Start the Services project,
Open postman and enter the uri, for example: http://localhost:7998/api/book, choose Get Method,
Send request.

Post book: add a book

![image](https://github.com/Lejuan60606/Library/assets/149588206/f5f8467b-803f-4dc1-ae0a-837e2a3a7836)

Book GetAll: get all books

![image](https://github.com/Lejuan60606/Library/assets/149588206/799899cc-2d41-4bec-8ed4-be78a0d5183e)

Book putBook:  update a book information

![image](https://github.com/Lejuan60606/Library/assets/149588206/ea1cb86d-b04c-4961-acb0-74b049f086b0)

Member getAll: get all members

![image](https://github.com/Lejuan60606/Library/assets/149588206/64588fab-4470-4aaf-9860-d0ab550bdb29)

BorrowTransaction BorrowBook; borrow a book, create a borrow transaction

![image](https://github.com/Lejuan60606/Library/assets/149588206/9c7a460a-a71c-46c3-82dc-32bf189b8711)

BorrowTransaction GetByMemberId: Get a borrow transaction list for the member

![image](https://github.com/Lejuan60606/Library/assets/149588206/172e2671-cbd2-4539-97f4-93a57f972b01)

BorrowTransaction ReturnBook: update a borrow transaction

![Uploading image.png…]()


