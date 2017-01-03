# Change log

# [1.0.0]
	#FIX
		- Propagate exception handled on execution of pipeline with sender.		
	#FEATURE
		- Support filters and handlers for each stage of pipeline.
		- Support multiple implementations of interface IPipelineJob<,> for the same class.
		- Add ErrorHandler for unexpected error on pipeline.
		- Remove the use of Reflection on pipeline execution.
		- Add PipelineOutput<T> class with the result and status of output.