using System;
using System.Collections.Generic;
using NUnit.Framework;
using FeeForPub;
using FluentAssertions;
using NSubstitute;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_AmountOfCustomerThatHaveToPay()
        {
            // arrange
            var customers = new List<Customer>
            {
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = false},
                new Customer(){IsMale = false}
            };
            int expected = 3;

            var stubCheckingFee = NSubstitute.Substitute.For<ICheckInFee>();
           // stubCheckingFee.GetFee(Arg.Any<Customer>()).Returns(100);
           stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == true)).Returns(100);
           stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false)).Returns(0);
            var pub = new Pub(stubCheckingFee);
            
            // act
            var customerThatHaveToPay = pub.CheckIn(customers);
            // assert
            customerThatHaveToPay.Should().Be(expected);
        }
        
        [Test]
        public void Test_Income()
        {
            // arrange
            var customers = new List<Customer>
            {
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = false},
                new Customer(){IsMale = false}
            };
            
            decimal expected = 300;
            
            var stubCheckingFee = NSubstitute.Substitute.For<ICheckInFee>();
            //stubCheckingFee.GetFee( Arg.Any<Customer>()).Returns(100);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == true)).Returns(100);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false)).Returns(0);
            
            var pub = new Pub(stubCheckingFee);
            
            // act
            pub.CheckIn(customers);
            var totalPay = pub.GetInCome();
            
            // assert
            totalPay.Should().Be(expected);
        }

        [Test] public void Test_LadiesNightOnlyOnThurButTodayIsNot()
        {
            // arrange
            
            var stubTime = Substitute.For<TimeProvider>();
            stubTime.Now.Returns(new DateTime(2021,5,21));
   
            var customers = new List<Customer>
            {
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = false},
                new Customer(){IsMale = false}
            };
            
            decimal expected = 400;
            
            var stubCheckingFee = NSubstitute.Substitute.For<ICheckInFee>();
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == true)).Returns(100);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false && stubTime.Now.DayOfWeek == DayOfWeek.Thursday)).Returns(0);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false && stubTime.Now.DayOfWeek != DayOfWeek.Thursday)).Returns(50);
            
            var pub = new Pub(stubCheckingFee);
            
            // act
            pub.CheckIn(customers);
            var totalPay = pub.GetInCome();
            
            // assert
            totalPay.Should().Be(expected);
        }
        
        [Test] public void Test_LadiesNightOnlyOnThur()
        {
            // arrange
            var stubTime = Substitute.For<TimeProvider>();
            stubTime.Now.Returns(new DateTime(2021,5,20));
            
            var customers = new List<Customer>
            {
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = true},
                new Customer(){IsMale = false},
                new Customer(){IsMale = false}
            };
            
            decimal expected = 300;
            
            var stubCheckingFee = NSubstitute.Substitute.For<ICheckInFee>();
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == true)).Returns(100);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false && stubTime.Now.DayOfWeek == DayOfWeek.Thursday)).Returns(0);
            stubCheckingFee.GetFee(Arg.Is<Customer>( x => x.IsMale == false && stubTime.Now.DayOfWeek != DayOfWeek.Thursday)).Returns(50);
            
            var pub = new Pub(stubCheckingFee);
            
            // act
            pub.CheckIn(customers);
            var totalPay = pub.GetInCome();
            
            // assert
            totalPay.Should().Be(expected);
        }
    }
}