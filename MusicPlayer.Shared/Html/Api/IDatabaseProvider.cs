namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The Database interface provides mechanisms by which browsers can store key/value pairs.
    /// </summary>
    public interface IDatabaseProvider // * Used for --> WebStorage (LocalStorage, SeasonStorage), Cookies
    {
        /// <summary>
        ///     When passed a key name, will return that key's value.
        /// </summary>
        /// <param name="key">A <see cref="string" /> containing the name of the key you want to retrieve the value of.</param>
        /// <returns>A <see cref="string" /> containing the value of the key.</returns>
        string GetItem(string key);

        /// <summary>
        ///     When passed a key name and value, will add that key to the database, or update that
        ///     key's value if it already exists.
        /// </summary>
        /// <param name="key">A <see cref="string" /> containing the name of the key you want to create/update.</param>
        /// <param name="value">A <see cref="string" /> containing the value you want to give the key you are creating/updating.</param>
        void SetItem(string key, string value);

        /// <summary>
        ///     When passed a key name, will remove that key from the database.
        /// </summary>
        /// <param name="key">A <see cref="string" /> containing the name of the key you want to remove.</param>
        void RemoveItem(string key);

        /// <summary>
        ///     When invoked, will empty all keys out of the database.
        /// </summary>
        void ClearAsync();
    }
}