# Formula.SimpleCore
Core utilities for the Formula Framework

## StatusBuilder
StatusBuilder is a class that encapsulates the return status of a particular process.
In addition to reporting whether or not the task was successful, it will provide additional meta data with data prepared or failure details.

## ConfigLoader
ConfigLoader provides a way of loading data from a json file into a strongly structured class, or from a delegate which will provide defaults in the absence of a file.