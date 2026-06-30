// Tests are stateless (each creates its own GroupAnalyzer), so they can run in parallel.
// Explicitly configuring this satisfies analyzer MSTEST0001.
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
