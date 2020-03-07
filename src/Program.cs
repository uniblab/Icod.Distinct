using System.Linq;

namespace Icod.Distinct {
	public static class Program {

		private const System.Int32 theBufferSize = 16384;


		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			if ( null == args ) {
				args = new System.String[ 0 ];
			}
			var len = args.Length;
			if ( 8 < len ) {
				PrintUsage();
				return 1;
			}
			System.String inputPathName = null;
			System.String outputPathName = null;
			System.String name = "EN-US";
			var ignoreCase = true;
			if ( 0 < len ) {
				len = len - 1;
				System.String @switch = null;
				System.Int32 i = -1;
				do {
					@switch = args[ ++i ];
					if ( "--input".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) ) {
						inputPathName = args[ ++i ].TrimToNull();
					} else if ( "--output".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) ) {
						outputPathName = args[ ++i ].TrimToNull();
					} else if ( "--name".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) ) {
						name = args[ ++i ].TrimToNull();
					} else if ( "--ignoreCase".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) ) {
						if ( !System.Boolean.TryParse( args[ ++i ].TrimToNull(), out ignoreCase ) ) {
							PrintUsage();
							return 1;
						}
					} else {
						PrintUsage();
						return 1;
					}
				} while ( i < len );
			}

			System.Func<System.String, System.Collections.Generic.IEnumerable<System.String>> reader = null;
			if ( System.String.IsNullOrEmpty( inputPathName ) ) {
				reader = a => ReadStdIn();
			} else {
				reader = a => ReadFile( a );
			}

			System.Action<System.String, System.Collections.Generic.IEnumerable<System.String>> writer = null;
			if ( System.String.IsNullOrEmpty( outputPathName ) ) {
				writer = ( a, b ) => WriteStdOut( b );
			} else {
				writer = ( a, b ) => WriteFile( a, b );
			}

			var read = reader( inputPathName ).ToList().Distinct( GetComparer( name, ignoreCase ) );
			if ( ( read != null ) && read.Any() ) {
				writer( outputPathName, read );
			}
			return 0;
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "Distinct.exe --help" );
			System.Console.Error.WriteLine( "Distinct.exe [--input inputFilePathName] [--output outputFilePathName] [--ignoreCase (true|false)] [--name cultureName]" );
			System.Console.Error.WriteLine( "Distinct.exe returns only unique lines of text from input." );
			System.Console.Error.WriteLine( "inputFilePathName and outputFilePathName may be relative or absolute paths." );
			System.Console.Error.WriteLine( "If inputFilePathName is omitted then input is read from StdIn." );
			System.Console.Error.WriteLine( "If outputFilePathName is omitted then output is written to StdOut." );
			System.Console.Error.WriteLine( "The name switch specifies the name of a culture, which is not case-sensitive." );
			System.Console.Error.WriteLine( "The ignoreCase switch should be true to specify that comparison operations be case-insensitive; false to specify that comparison operations be case-sensitive." );
		}

		private static System.Collections.Generic.IEqualityComparer<System.String> GetComparer( System.String name, System.Boolean ignoreCase ) {
			return System.StringComparer.Create( System.Globalization.CultureInfo.GetCultureInfo( name.TrimToNull() ), ignoreCase );
		}

		#region io
		private static System.Collections.Generic.IEnumerable<System.String> ReadStdIn() {
			var line = System.Console.In.ReadLine();
			while ( null != line ) {
				line = line.TrimToNull();
				if ( null != line ) {
					yield return line;
				}
				line = System.Console.In.ReadLine();
			}
		}
		private static System.Collections.Generic.IEnumerable<System.String> ReadFile( System.String filePathName ) {
			filePathName = filePathName.TrimToNull();
			if ( System.String.IsNullOrEmpty( filePathName ) ) {
				throw new System.ArgumentNullException( "filePathName" );
			}
			using ( var file = System.IO.File.Open( filePathName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read ) ) {
				using ( var reader = new System.IO.StreamReader( file, System.Text.Encoding.UTF8, true, theBufferSize, true ) ) {
					var line = reader.ReadLine();
					while ( null != line ) {
						line = line.TrimToNull();
						if ( null != line ) {
							yield return line;
						}
						line = reader.ReadLine();
					}
				}
			}
		}

		private static void WriteStdOut( System.Collections.Generic.IEnumerable<System.String> data ) {
			foreach ( var datum in data ) {
				System.Console.Out.WriteLine( datum );
			}
		}
		private static void WriteFile( System.String filePathName, System.Collections.Generic.IEnumerable<System.String> data ) {
			filePathName = filePathName.TrimToNull();
			if ( System.String.IsNullOrEmpty( filePathName ) ) {
				throw new System.ArgumentNullException( "filePathName" );
			}
			using ( var file = System.IO.File.Open( filePathName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None ) ) {
				file.Seek( 0, System.IO.SeekOrigin.Begin );
				using ( var writer = new System.IO.StreamWriter( file, System.Text.Encoding.UTF8, theBufferSize, true ) ) {
					foreach ( var datum in data ) {
						writer.WriteLine( datum );
					}
					writer.Flush();
				}
				file.Flush();
				file.SetLength( file.Position );
			}
		}
		#endregion io

		private static System.String TrimToNull( this System.String @string ) {
			if ( System.String.IsNullOrEmpty( @string ) ) {
				return null;
			}
			@string = @string.Trim();
			return System.String.IsNullOrEmpty( @string )
				? null
				: @string
			;
		}

	}

}