using System;

namespace UfrawParallel.Core
{
	/// <summary>
	/// Contains handlers for the ufraw-batch console output.
	/// </summary>
	public sealed class UfrawOutputHandlers
	{
		/// <summary>
		/// Standard output handler.
		/// </summary>
		public EventHandler<string> OutputChangedHandler { get; }

		/// <summary>
		/// Error output handler.
		/// </summary>
		public EventHandler<string> ErrorChangedHandler { get; }

		/// <summary>
		/// Combined output handler.
		/// </summary>
		public EventHandler<string> CombinedOutputChangedHandler { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="UfrawOutputHandlers"/>.
		/// </summary>
		/// <param name="outputChangedHandler">Standard output handler to be set.</param>
		/// <param name="errorChangedHandler">Error output handler.</param>
		/// <param name="combinedOutputChangedHandler">Combined output handler.</param>
		public UfrawOutputHandlers(EventHandler<string> outputChangedHandler, EventHandler<string> errorChangedHandler, EventHandler<string> combinedOutputChangedHandler)
		{
			this.OutputChangedHandler = outputChangedHandler;
			this.ErrorChangedHandler = errorChangedHandler;
			this.CombinedOutputChangedHandler = combinedOutputChangedHandler;
		}

		/// <summary>
		/// Checks if at least one handler is set in the <see cref="UfrawOutputHandlers"/> instance.
		/// </summary>
		/// <returns>True, if at least one handler is not null; otherwise, false.</returns>
		public bool AnyHandlerSet()
		{
			return (OutputChangedHandler != null) ||
				(ErrorChangedHandler != null) ||
				(CombinedOutputChangedHandler != null);
		}
	}
}
