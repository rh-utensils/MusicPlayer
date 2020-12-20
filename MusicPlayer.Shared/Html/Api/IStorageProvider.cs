using System;
using System.Threading.Tasks;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="IStorageProvider" /> interface represents a system for storing and retrieving
    ///     files and folders. Files and folders may be stored either by the local file system or by a remote
    ///     service like Microsoft OneDrive.
    /// </summary>
    public interface IStorageProvider // * Used for --> NativeFS Storage, NativeFS Electron, IndexedDB, Download, GoogleDrive, OneDrive, Dropbox, ...
    {
        /// <summary>
        ///     Gets a user-friendly name for the current provider of files and folders.
        /// </summary>
        /// <value>The user-friendly name for the provider.</value>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the version number of the data in the app data store.
        /// </summary>
        /// <value>The version number of the data.</value>
        UInt32 Version { get; }

        /// <summary>
        ///     Gets the root folder in the data store.
        /// </summary>
        /// <value>The file system folder that contains the files.</value>
        IFolder Folder { get; }

        /// <summary>
        ///     Removes all data from the data store.
        /// </summary>
        /// <returns>An object that is used to manage the asynchronous clear operation.</returns>
        Task ClearAsync();

        /// <summary>
        ///     Sets the version number of the data in the data store.
        /// </summary>
        /// <param name="desiredVersion">The new version number.</param>
        /// <returns>An object that is used to manage the asynchronous set version operation.</returns>
        Task SetVersionAsync(UInt32 desiredVersion);

        /// <summary>
        ///     Sends a <see cref="OnDataChanged" /> event to all registered event handlers.
        ///     <para />
        ///     You can use the <see cref="SignalDataChanged" /> method to fire a <see cref="OnDataChanged" /> event that you can use
        ///     to signal other code in your to reload the local state.
        /// </summary>
        void SignalDataChanged();

        /// <summary>
        ///     Occurs when data is changed.
        /// </summary>
        event EventHandler OnDataChanged;
    }
}