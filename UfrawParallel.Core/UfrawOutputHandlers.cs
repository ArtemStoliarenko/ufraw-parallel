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
		/// True if at least one handler is not null; otherwise, false.
		/// </summary>
		public bool AnyHandlerSet { get; }

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

			this.AnyHandlerSet = (OutputChangedHandler != null) || (ErrorChangedHandler != null) || (CombinedOutputChangedHandler != null);
		}
	}
}
