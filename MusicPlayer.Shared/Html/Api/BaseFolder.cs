using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Uno.Extensions;

namespace MusicPlayer.Shared.Html.Api
{
    /// <inheritdoc />
    public abstract class Folder : IFolder
    {
        /// <inheritdoc />
        public string Name => System.IO.Path.GetFileName(Path.TrimEnd(System.IO.Path.DirectorySeparatorChar));

        /// <inheritdoc />
        public string Path { get; private set; }

        /// <inheritdoc />
        public DateTimeOffset DateCreated { get; private set; }

        /// <inheritdoc />
        public IStorageProvider Provider { get; private set; }

        //TODO: Get the mime type to an extension to create Files
        //* https://github.com/samuelneff/MimeTypeMap
        /// <inheritdoc />
        public abstract Task<IFile> CreateFileFromBytesAsync(string name, byte[] content);

        /// <inheritdoc />
        public async Task<IFile> CreateFileFromStreamAsync(string name, Stream dataRequested)
        {
            var bytes = await dataRequested.ReadBytesAsync();
            var file = await CreateFileFromBytesAsync(name, bytes);
            return file;
        }

        /// <inheritdoc />
        public abstract Task<IFile> CreateFileFromUriAsync(string name, Uri uri); //* Use Fetch API

        /// <inheritdoc />
        public abstract Task<IFolder> CreateFolderAsync(string name);
        
        /// <inheritdoc />
        public async Task<IFile> GetFileAsync(string name)
        {
            var path = System.IO.Path.Combine(Path, name);
            var file = await GetFileFromPathAsync(path);
            return file;
        }

        /// <inheritdoc />
        public abstract Task<IFile> GetFileFromPathAsync(string path);

        /// <inheritdoc />
        public abstract Task<ReadOnlyCollection<IFile>> GetFilesAsync();

        /// <inheritdoc />
        public async Task<IFolder> GetFolderAsync(string name)
        {
            var path = System.IO.Path.Combine(Path, name);
            var folder = await GetFolderFromPathAsync(path);
            return folder;
        }

        /// <inheritdoc />
        public abstract Task<IFolder> GetFolderFromPathAsync(string path);

        /// <inheritdoc />
        public abstract Task<ReadOnlyCollection<IFolder>> GetFoldersAsync();

        /// <inheritdoc />
        public async Task<IFolder> GetParentAsync()
        {
            var path = Path.TrimEnd(System.IO.Path.DirectorySeparatorChar);
            var directory = System.IO.Path.GetDirectoryName(path);
            var parent = await Provider.Folder.GetFolderFromPathAsync(directory);
            return parent;
        }

        /// <inheritdoc />
        public abstract Task RenameAsync(string name);

        /// <inheritdoc />
        public abstract Task DeleteAsync();
    }
}