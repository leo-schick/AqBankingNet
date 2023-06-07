AqBanking
=========

AqBanking is a library for online banking and financial
applications. 

The NuGet package is a wrapper using the linux _aqbanking_ library. For install instructions below.

Not all functions of the official library are be implemented into the .NET library wrapper. When there
something you miss, feel free to send in a Pull Request on [GitHub](https://github.com/leo-schick/AqBankingNet).

The homepage of AqBanking is https://www.aqbanking.de/

1. AqBanking
------------

AqBanking has three major goals which are described in the following paragraphs.

### 1.1. Generic Online Banking Interface

The intention of AqBanking is to provide a middle layer between the program
and the various Online Banking libraries (e.g. AqHBCI, OpenHBCI etc).

The real work is done in so-called banking backends. See chapter 3 for a
list of supported backends.

### 1.2. Generic Financial Data Importer/Exporter Framework

AqBanking uses various plugins to simplify import and export of financial
data. It also provides the administration of profiles on a per import/export
plugin basis.

Currently there are plugins for the following formats:

- Importers:
   - DTAUS (German financial format)
   - SWIFT (MT940 and MT942)
   - OFX
   - CSV
   - OpenHBCI1 transactions
   - ERI
   - Q43
   - XML (various formats like SEPA-PAIN, CAMT, OFX)

- Exporters
   - DTAUS (German financial format)
   - CSV
   - XML (various formats like SEPA-PAIN, CAMT, OFX)

### 1.3. Bank/Account Information

AqBanking supports plugins which allow lookup of
bank code/ account id pair validity.

Currently AqBanking provides informations about:
- ca 20,000 German banks

2. Supported Platforms
----------------------

AqBanking uses the library Gwenhywfar (https://www.aqbanking.de/) for
abstraction of the underlying system. So it should work on any system for
which Gwenhywfar is available.

This includes (but is not limited to):
- Linux (of course ;-)
- Windows
- MacOSX 10.5 and newer
- most POSIX systems (such as the BSDs) should also be supported,
  however, this is untested

3. Supported Backends
---------------------

AqBanking includes all its currently known banking backends.


### 3.1. HBCI

The backend AqHBCI provides support for the German online banking protocol
called "Homebanking Computer Interface". It is a national standard provided
by most German credit institutes.

The following security media are supported:
- DDV chipcard (DES-DES-Verfahren)
- RSA chipcard (RSA-DES-Hybrid mode)
- OpenHBCI keyfile (either OpenHBCI 1 or 2, this medium allows continued use
  with OpenHBCI in parallel)
- PIN/TAN (PIN/TAN mode using HTTP over SSL)

This backend supports the HBCI versions 2.01, 2.10, 2.20 and 3.00


### 3.2. OFX Direct Connect

This backend provides support for an online banking protocol used in the
United States, Canada and maybe in the United Kingdom.


### 3.3. EBICS

EBICS is the successor of the German banking protocol FTAM. It is used in commercial
environments.


### 3.4. Paypal

This backend uses Paypal's native API for retrieving transactions.


### 3.5. None

This is a fallback module which can be used by applications for accounts which
are not managed by any online banking backend.


Install
-------

You need library `libaqbanking.so` installed on your machine.

On Debian, you can install it via `apt install -y libaqbanking-dev`.
