using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     Manages folders and their contents and provides information about them.
    /// </summary>
    public interface IFolder
    {
        /// <summary>
        ///     Gets the name of the current folder.
        /// </summary>
        /// <value>The name of the current folder.</value>
        string Name { get; }

        /// <summary>
        ///     Gets the full path of the current folder in the file system, if the path is available.
        /// </summary>
        /// <value>The full path of the current folder in the file system, if the path is available.</value>
        string Path { get; }

        /// <summary>
        ///     Gets the date and time that the current folder was created.
        ///     <para />
        ///     If the date property isn't set, this value defaults to 0 which can be translated into misleading dates in different programming
        ///     languages. In JavaScript, for example, 0 translates to December 16, 1600. You should always check that this property is a real
        ///     value and not 0.
        /// </summary>
        /// <value>The date and time that the current folder was created as type <see cref="DateTime" />.</value>
        DateTimeOffset DateCreated { get; }

        /// <summary>
        ///     Gets the <see cref="IStorageProvider" /> object that contains info about the service that stores the current folder.
        /// </summary>
        /// <value>
        ///     The <see cref="IStorageProvider" /> object that contains info about the service that stores the current folder. The folder may be stored by the local
        ///     file system or by a remote service like Microsoft OneDrive.
        /// </value>
        IStorageProvider Provider { get; }

        /// <summary>
        ///     Creates a <see cref="IFile" /> to represent the specified stream of data with the specified name in the current
        ///     folder. This method lets the app produce the data on-demand by specifying a function to be invoked when the
        ///     <see cref="IFile" /> that represents the stream is first accessed.
        /// </summary>
        /// <param name="name">The name of the new file to create in the current folder.</param>
        /// <param name="dataRequested">
        ///     The function that should be invoked when the <see cref="IFile" /> that is returned is first accessed. This function should produce the data
        ///     stream represented by the returned <see cref="IFile" /> and lets the app produce data on-demand instead of writing the contents of the file
        ///     at creation time.
        /// </param>
        /// <returns>When this method completes, it returns a <see cref="IFile" /> object that represents the new stream of data.</returns>
        Task<IFile> CreateFileFromStreamAsync(string name, Stream dataRequested);

        /// <summary>
        ///     Creates a <see cref="IFile" /> to represent a stream of data from the specified Uniform Resource Identifier (URI) resource
        ///     with the specified name in the current folder. This method lets the app download the data on-demand when the <see cref="IFile" />
        ///     that represents the stream is first accessed.
        /// </summary>
        /// <param name="name">The name of the new file to create in the current folder.</param>
        /// <param name="uri">The Uniform Resource Identifier (URI) of the resource used to create the <see cref="IFile" />.</param>
        /// <returns>When this method completes, it returns a <see cref="IFile" /> object that represents the Uniform Resource Identifier (URI) resource.</returns>
        Task<IFile> CreateFileFromUriAsync(string name, Uri uri);

        /// <summary>
        ///     Creates a <see cref="IFile" /> to represent a stream of data from the specified array of bytes
        ///     with the specified name in the current folder.
        /// </summary>
        /// <param name="name">The name of the new file to create in the current folder.</param>
        /// <param name="content">The byte array of the resource used to create the <see cref="IFile" />.</param>
        /// <returns>When this method completes, it returns a <see cref="IFile" /> that represents the new file.</returns>
        Task<IFile> CreateFileFromBytesAsync(string name, byte[] content);

        /// <summary>
        ///     Creates a new subfolder with the specified name in the current folder.
        /// </summary>
        /// <param name="name">The name of the new subfolder to create in the current folder.</param>
        /// <returns>When this method completes, it returns a <see cref="IFolder" /> that represents the new subfolder.</returns>
        Task<IFolder> CreateFolderAsync(string name);

        /// <summary>
        ///     Gets the file with the specified name from the current folder.
        /// </summary>
        /// <param name="name">The name (or path relative to the current folder) of the file to get.</param>
        /// <returns>When this method completes successfully, it returns a <see cref="IFile" /> that represents the specified file.</returns>
        Task<IFile> GetFileAsync(string name);

        /// <summary>
        ///     Gets the file that has the specified absolute path in the file system.
        /// </summary>
        /// <param name="path">The absolute path in the file system (not the Uri) of the folder to get.</param>
        /// <returns>When this method completes successfully, it returns a <see cref="IFile" /> that represents the specified file.</returns>
        Task<IFile> GetFileFromPathAsync(string path);

        /// <summary>
        ///     Gets the files in the current folder.
        /// </summary>
        /// <returns>
        ///     When this method completes successfully, it returns a list of the files in the current folder. The list is of type 
        ///     IReadOnlyList&lt;<see cref="IFile" />&gt;. Each file in the list is represented by a <see cref="IFile" /> object.
        /// </returns>
        Task<ReadOnlyCollection<IFile>> GetFilesAsync();

        /// <summary>
        ///     Gets the subfolder with the specified name from the current folder.
        /// </summary>
        /// <param name="name">The name (or path relative to the current folder) of the subfolder to get.</param>
        /// <returns>When this method completes successfully, it returns a <see cref="IFolder" /> that represents the specified subfolder.</returns>
        Task<IFolder> GetFolderAsync(string name);

        /// <summary>
        ///     Gets the folder that has the specified absolute path in the file system.
        /// </summary>
        /// <param name="path">The absolute path in the file system (not the Uri) of the folder to get.</param>
        /// <returns>When this method completes successfully, it returns a <see cref="IFolder" /> that represents the specified folder.</returns>
        Task<IFolder> GetFolderFromPathAsync(string path);

        /// <summary>
        ///     Gets the subfolders in the current folder.
        /// </summary>
        /// <returns>
        ///     When this method completes successfully, it returns a list of the subfolders in the current folder. The list is of type
        ///     IReadOnlyList&lt;<see cref="IFolder" />&gt;. Each folder in the list is represented by a <see cref="IFolder" /> object.
        /// </returns>
        Task<ReadOnlyCollection<IFolder>> GetFoldersAsync();

        /// <summary>
        ///     Gets the parent folder of the current folder.
        /// </summary>
        /// <returns>When this method completes, it returns the parent folder as a <see cref="IFolder" />.</returns>
        Task<IFolder> GetParentAsync();

        /// <summary>
        ///     Renames the current folder.
        /// </summary>
        /// <param name="name">The desired, new name for the current folder.</param>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task RenameAsync(string name);

        /// <summary>
        ///     Deletes the current folder.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task DeleteAsync();
    }
}