using System;
using System.IO;
using System.Threading.Tasks;
using Uno.Foundation;
using Windows.Storage.Streams;

namespace MusicPlayer.Shared.Html.Api
{
    /// <inheritdoc />
    public abstract class File : IFile
    {
        /// <summary>
        ///     Stores the created object Uri to avoid duplicates.
        /// </summary>
        /// <value>An object Uri of the file if one was previously created.</value>
        private Uri _objectUri { get; set; }

        /// <inheritdoc />
        public string Name => System.IO.Path.GetFileName(Path);

        /// <inheritdoc />
        public string DisplayName => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <inheritdoc />
        public string FileType => System.IO.Path.GetExtension(Path) is var extension && string.IsNullOrEmpty(extension) ? null : extension.Remove(0, 1);

        /// <inheritdoc />
        public string ContentType { get; private set; } 

        /// <inheritdoc />
        public string Path { get; private set; }

        /// <inheritdoc />
        public ulong Size { get; private set; }

        /// <inheritdoc />
        public DateTimeOffset DateCreated { get; private set; }

        /// <inheritdoc />
        public DateTimeOffset DateModified { get; private set; }

        /// <inheritdoc />
        public IStorageProvider Provider { get; private set; }

        /// <inheritdoc />
        public async Task CopyAndReplaceAsync(IFile fileToReplace)
        {
            await fileToReplace.ReplaceWithAsync(this);
        }

        /// <inheritdoc />
        public async Task<IFile> CopyAsync(IFolder destinationFolder)
        {
            return await CopyAsync(destinationFolder, DisplayName);
        }

        /// <inheritdoc />
        public async Task<IFile> CopyAsync(IFolder destinationFolder, string displayName)
        {
            var name = string.Concat(displayName, '.', FileType);
            var file = await destinationFolder.CreateFileFromBytesAsync(name, new byte[0]);

            await CopyAndReplaceAsync(file);

            return file;
        }

        /// <inheritdoc />
        public async Task MoveAndReplaceAsync(IFile fileToReplace)
        {
            await CopyAndReplaceAsync(fileToReplace);
            OverridePropertiesFrom(fileToReplace);
        }

        /// <inheritdoc />
        public async Task MoveAsync(IFolder destinationFolder)
        {
            await MoveAsync(destinationFolder, DisplayName);
        }

        /// <inheritdoc />
        public async Task MoveAsync(IFolder destinationFolder, string displayName)
        {
            var file = await CopyAsync(destinationFolder, displayName);
            OverridePropertiesFrom(file);
        }

        /// <inheritdoc />
        public async Task ReplaceWithAsync(IFile file)
        {
            this._objectUri = null;
            this.ContentType = file.ContentType;
            this.Size = file.Size;
            this.DateCreated = file.DateCreated;
            this.DateModified = file.DateModified;
            file.RevokeObjectURL();

            await ReplaceDataFromAsync(file);
        }

        /// <summary>
        ///     Replaces the current file content with a copy of the specified file content.
        /// </summary>
        /// <param name="file">The file to replace current file content.</param>
        /// <returns>No object or value is returned when this method completes.</returns>
        protected abstract Task ReplaceDataFromAsync(IFile file);

        /// <summary>
        ///     Replaces the current file properties with a copy of the specified file properties.
        /// </summary>
        /// <param name="file">The file to replace current file properties.</param>
        protected void OverridePropertiesFrom(IFile file)
        {
            this._objectUri = null;
            this.ContentType = file.ContentType;
            this.Path = file.Path;
            this.Size = file.Size;
            this.DateCreated = file.DateCreated;
            this.DateModified = file.DateModified;
            this.Provider = file.Provider;
            file.RevokeObjectURL();
        }

        /// <inheritdoc />
        public abstract Task RenameAsync(string name);

        /// <inheritdoc />
        public abstract Task DeleteAsync();

        /// <inheritdoc />
        public async Task<Stream> OpenStreamForReadAsync()
        {
            var objectUri = await GetObjectUriAsync();
            var streamReference = RandomAccessStreamReference.CreateFromUri(objectUri);
            var content = await streamReference.OpenReadAsync();
            var stream = content.AsStreamForRead();

            RevokeObjectURL();

            return stream;
        }

        /// <inheritdoc />
        public async Task<IFolder> GetParentAsync()
        {
            var directory = System.IO.Path.GetDirectoryName(Path);
            var parent = await Provider.Folder.GetFolderFromPathAsync(directory);

            return parent;
        }

        /// <summary>
        ///     The <see cref="GetObjectUriAsync" /> method returns a <see cref="Uri" /> representing an object, that
        ///     will be created by the <see cref="CreateObjectUriAsync" /> method. If a object uri already exists that
        ///     represents this object, the <see cref="GetObjectUriAsync" /> method returns the saved <see cref="Uri" />.
        ///     The URL lifetime is tied to the document in the window on which it was created.
        ///     The new object URL represents the specified <see cref="IFile" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> that represents an <see cref="Uri" /> that can be used to reference the contents of the
        ///     specified source object.
        /// </returns>
        public async Task<Uri> GetObjectUriAsync()
        {
            if (_objectUri != null) return _objectUri;
            else return await CreateObjectUriAsync();
        }

        /// <summary>
        ///     The <see cref="CreateObjectUriAsync" /> method creates a <see cref="Uri" /> representing an object.
        ///     The URL lifetime is tied to the document in the window on which it was created.
        ///     The new object URL represents the specified <see cref="IFile" /> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> that represents an <see cref="Uri" /> that can be used to reference the contents of the
        ///     specified source object.
        /// </returns>
        protected abstract Task<Uri> CreateObjectUriAsync();

        /// <inheritdoc />
        public void RevokeObjectURL()
        {
            if (_objectUri == null) return;

            var uri = WebAssemblyRuntime.EscapeJs(_objectUri.ToString());
            WebAssemblyRuntime.InvokeJS($"URL.revokeObjectURL({uri})");
            _objectUri = null;
        }
    }
}