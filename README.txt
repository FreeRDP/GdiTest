==========================================
GdiTest: A GDI Implementation Tester
==========================================

GdiTest is a program written in C# that generates test data
readily usable in unit tests. GDI has many subtleties,
and implementing it without extensive unit testing
is often prone to errors.

Prerequisites
------------------------------------------

In order to compile and run GdiTest, you will need:

* Windows
* Gtk# for .NET
* Mono for Windows, Gtk#
* MonoDevelop for Windows

Actually, you can compile and run GdiTest on Linux or Mac OS X,
but the code will detect the absence of gdi32.dll and call
function stubs that do nothing instead.

In the future, however, it will be possible to wrap alternative
GDI implementations in C# so that they can be easily compared with
other implementations, such as the one provided in gdi32.dll. Since
gdi32.dll is only usable on Windows, it would also be a possibility
to make one instance of GdiTest running on Windows expose a service
to be used by remote instances running on Linux, through C# remoting.

Compilation
------------------------------------------

Once you have installed all prerequisites, simply open the solution
file in MonoDevelop and click run. That is simply it.

Usage
------------------------------------------

You will notice that GdiTest has a dropdown box that allows you to
select a particular test suite. Select the one you like, and click
"dump", you will then get the test data in the text box below.

To know more about each test case, look at the source code itself.
