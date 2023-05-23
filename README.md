# Test Task
Data structure consists of named collections (name is an array key).
Collection is an array of elements of the same type (arrays can be nested for any level).
Collection elements can be of any data type allowed.
Need to implement following functionality to operate described data structures:

1. Adding (Merge) of structures.
   Elements duplication is forbidden, comparision condition - complete equality of all fields.
   Need to support ability to change comparision algorithm flexible.
   Duplicates in target collection are acceptable, merging should not affect them.
2. Subtraction (Cut) of structures.
   All elements of subtracted structure should be removed from target one.
   Using comparision algorithm is same as in previous point.
   Target structure elements didn't match elements from subtracted should remain unchanged.