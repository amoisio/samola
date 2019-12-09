Base classes
============
CalculatedEnumerable

Enumerables
============
PrimeNumbers

Utilities
============

EnumerationState object encapsulates the state of the enumerable before a cached or calculated value is yielded to the consumer. This allows you to create checks on the iteration state to determine if the value can/should be yielded, and stop the enumerable from yielding the value if needed. Particularly, a class inheriting the CalculatedEnumerable can use a Limit object to check if a given limit condition has been reached in the enumeration. This is done in the LimitOk method of the Limit class.



