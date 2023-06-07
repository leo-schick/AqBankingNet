Gwenhywfar
==========

This is **Gwenhywfar**, a multi-platform helper library for networking and
security applications and libraries.

The NuGet package is a wrapper using the linux library [_gwenhywfar_](https://github.com/aqbanking/aqbanking). For install instructions see below.

Not all functions of the official library are be implemented into the .NET library wrapper. When there
something you miss, feel free to send in a Pull Request on [GitHub](https://github.com/leo-schick/AqBankingNet). 

Features
--------

This library is written in C, and it follows the object-oriented
programming paradigm for most of its parts.  The header files can
directly be used from C++, too.

Gwenhywfar includes the following features:

- Basic Data types for binary buffers, ring buffers, linked lists, 
  error objects, string lists (src/base/), buffered IO operations (src/io/)

- Macros for typesafe list management

- OS abstraction functions for directory and Internet address handling
  and library loading (src/os/)

- Networking functions which allow to manage many connections to be used
  economically even in single threaded applications

- High-level functions for parsing files with a simplified "XML-like"
  format and accessing them like a hierarchical database (src/parser/)
  It is able to process valid XML files, too.

- High-level cryptographic functions on top of OpenSSL functionality
  (src/crypt/)

- Support for interprocess communication (HTTP on top of SSL or plain
  TCP/UDP sockets, with or without certificates for clients and/or servers)

- A tool to generate simple data containers from XML files. It automatically
  generates getters, setters, constructor, destructor, deep-copy function,
  usage counter handling, modification tracking, functions for reading from 
  and writing to GWEN_DBs (used by AqBanking to create the transaction class)

Versioning
----------
The library versioning follows the versioning of the library _gwenhywfar_ it has been
tested with, with an additional [Revision](https://learn.microsoft.com/en-us/dotnet/api/system.version) part
which represents the version of this wrapper library and is incremented at each new release.

Install
-------

You need library `libgwenhywfar.so` installed on your machine.

On Debian, you can install it via `apt install -y libgwenhywfar-core-dev`.
