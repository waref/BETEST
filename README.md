# Documentation for Techincal Test

## Description

This repository contains a solution for Back-End exam for
[Lucca](https://www.lucca.fr/) , done by [Heni Waref](https://www.linkedin.com/in/waref-heni/) for fun , maybe ! :stuck_out_tongue_closed_eyes:

## Usage

### Installing

Run LuccaDevises.exe in cmd with paramter as text file containg a specific data as

```
EUR;550;JPY
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571
```

### Prject structure

Here's a folder structure for a Pandoc document:

```
LuccaCurrrency/             # Main solution.
|- BusinessLayer/           # The engine project : Expose calculation services and execute current example calculation steps in process: ExchangeCalculatorProcess.
|- Documentation/           # Markdowns documentation for the project.
|- PresentationLayer/       # Console Application, uses ConsoleDataAdapterProcess to return the response to console.
|- ServicesLayer            # Project that Expose data validation and extractio, it uses StringsFormatValidatorProcess to run in chain the validation and extraction.
|- UnitTests                # Unit test for BusinessLayer and ServicesLayer.
```

### Usage Principe

* Each layer (execpt test project) contains a chain of responsability using a Token Object.
* BusinessLayer & PresentationLayer expose services undividually for other usages .
* The current algorithm is suitable for this example ( undirected and unweighted ). 
* In case of new request to support also a weighted graph , I recommand to keep usage of Matrix generation in BusinessLayer and use strategy pattern to switch between algorithms depending on context (condition).
* I used Autofac Ioc for dependcy injection to ensure the depency invertion principe ( the D in SOLID principe).
### Unit tests 

 I used unit test first (TDD) with the help of MsTest and Moq for mocking objects and their resyrns.
 
 
```yml
---
title: Technical specification
author: Heni Wref
rights:  Open source
language: en-US

---
```
