using System;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="IFile" /> interface provides information about files and allows JavaScript in a web page to
    ///     access their content.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        ///     Returns the name of the file represented by a <see cref="IFile" /> object. For security reasons, the path is
        ///     excluded from this property.
        /// </summary>
        /// <value>
        ///     A <see cref="string" />, containing the name of the file without path, such as "My Resume.rtf".
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Gets a user-friendly name for the file.
        /// </summary>
        /// <value>
        ///     A <see cref="string" />, containing the user-friendly name for the file.
        /// </value>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the type (file name extension) of the file.
        /// </summary>
        /// <value>
        ///     A <see cref="string" />, containing the file name extension of the file.
        /// </value>
        string FileType { get; }

        /// <summary>
        ///     Returns the media type (MIME) of the file represented by a <see cref="IFile" /> object.
        /// </summary>
        /// <value>
        ///     A <see cref="string" />, containing the media type (MIME) indicating the type of the file, for example
        ///     "image/png" for PNG images
        /// </value>
        string ContentType { get; }

        /// <summary>
        ///     Gets the full file path of the current file in the storage, if the file has a path.
        /// </summary>
        /// <value>The full path of the file, if the file has a path in the storage.</value>
        string Path { get; }

        /// <summary>
        ///     Returns the size of a file in bytes.
        /// </summary>
        /// <value>An <see cref="ulong" /></value>
        ulong Size { get; }

        /// <summary>
        ///     Gets the date and time when the current file was created.
        ///     <para />
        ///     If the date property isn't set, this value defaults to 0 which can be translated into misleading dates in different
        ///     programming languages. In JavaScript, for example, 0 translates to December 16, 1600. You should always check that
        ///     this property is a real value and not 0.
        /// </summary>
        /// <value>The date and time when the current file was created.</value>
        DateTimeOffset DateCreated { get; }

        /// <summary>
        ///     The <see cref="DateModified" /> read-only property returns the last modified date of the file.
        ///     Files without a known last modified date returns the current date .
        /// </summary>
        /// <value>
        ///     A <see cref="DateTimeOffset" /> object indicating the date and time at which the file was last modified
        /// </value>
        DateTimeOffset DateModified { get; }

        /// <summary>
        ///     Gets the <see cref="IStorageProvider" /> object that contains info about the service that stores the current file.
        /// </summary>
        /// <value>
        ///     The <see cref="IStorageProvider" /> object that contains info about the service that stores the current file. The file
        ///     may be stored by the local file system or by a remote service like Microsoft OneDrive.
        /// </value>
        IStorageProvider Provider { get; }

        /// <summary>
        ///     Replaces the specified file with a copy of the current file.
        /// </summary>
        /// <param name="fileToReplace">The file to replace.</param>
        /// <returns>No object or value is returned when this method completes.</returns>
        Task CopyAndReplaceAsync(IFile fileToReplace);

        /// <summary>
        ///     Creates a copy of the file in the specified folder.
        /// </summary>
        /// <param name="destinationFolder">The destination folder where the copy of the file is created.</param>
        /// <returns>
        ///     When this method completes, it returns a <see cref="IFile" /> that represents the copy of the file created in the destinationFolder.
        /// </returns>
        Task<IFile> CopyAsync(IFolder destinationFolder);

        /// <summary>
        ///     Creates a copy of the file in the specified folder and renames the copy.
        /// </summary>
        /// <param name="destinationFolder">The destination folder where the copy of the file is created.</param>
        /// <param name="displayName">The new name for the copy of the file created in the destinationFolder.</param>
        /// <returns>
        ///     When this method completes, it returns a <see cref="IFile" /> that represents the copy of the file created in the destinationFolder.
        /// </returns>
        Task<IFile> CopyAsync(IFolder destinationFolder, string displayName);

        /// <summary>
        ///     Moves the current file to the location of the specified file and replaces the specified file in that location.
        /// </summary>
        /// <param name="fileToReplace">The file to replace.</param>
        /// <returns>No object or value is returned by this method.</returns>
        Task MoveAndReplaceAsync(IFile fileToReplace);

        /// <summary>
        ///     Moves the current file to the specified folder.
        /// </summary>
        /// <param name="destinationFolder">The destination folder where the file is moved.</param>
        /// <returns>No object or value is returned by this method.</returns>
        Task MoveAsync(IFolder destinationFolder);

        /// <summary>
        ///     Moves the current file to the specified folder and renames the file according to the desired name.
        /// </summary>
        /// <param name="destinationFolder">The destination folder where the file is moved.</param>
        /// <param name="displayName">
        ///     The desired name of the file after it is moved.
        ///     <para />
        ///     If there is an existing file in the destination folder that already has the specified displayName, it
        ///     will generate a unique name for the file.
        /// </param>
        /// <returns>No object or value is returned by this method.</returns>
        Task MoveAsync(IFolder destinationFolder, string displayName);

        /// <summary>
        ///     Replaces the current file with a copy of the specified file.
        /// </summary>
        /// <param name="file">The file to replace current file.</param>
        /// <returns>No object or value is returned when this method completes.</returns>
        Task ReplaceWithAsync(IFile file);

        /// <summary>
        ///     Renames the current file.
        /// </summary>
        /// <param name="name">The desired, new name of the current item.</param>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task RenameAsync(string name);

        /// <summary>
        ///     Deletes the current file.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task DeleteAsync();

        /// <summary>
        ///     Retrieves a <see cref="Stream" /> for reading from a specified file.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> that represents the asynchronous read operation.
        /// </returns>
        Task<Stream> OpenStreamForReadAsync();

        /// <summary>
        ///     Gets the parent folder of the current file.
        /// </summary>
        /// <returns>When this method completes, it returns the parent folder as a <see cref="IFolder" />.</returns>
        Task<IFolder> GetParentAsync();

        /// <summary>
        ///     The <see cref="createObjectUriAsync" /> method creates a <see cref="Uri" /> representing an object.
        ///     The URL lifetime is tied to the document in the window on which it was created.
        ///     The new object URL represents the specified <see cref="IFile" /> object.
        ///     <para/>
        ///     To release an object URL, call <see cref="revokeObjectURL" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> that represents an <see cref="Uri" /> that can be used to reference the contents of the
        ///     specified source object.
        /// </returns>
        Task<Uri> createObjectUriAsync();

        /// <summary>
        ///     The <see cref="revokeObjectURL" /> method releases an existing object URL which was
        ///     previously created by calling <see cref="createObjectUriAsync" />. Call this method when you've finished
        ///     using an object URL to let the browser know not to keep the reference to the file any longer.
        /// </summary>
        void revokeObjectURL();
    }
}