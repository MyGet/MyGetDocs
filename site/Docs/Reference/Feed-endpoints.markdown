# Feed endpoints

This page explains the different feed endpoints available on MyGet. Depending on the client you are using (NuGet.exe, Orchard CMS, ...), these endpoints can be configured as the package source or as the publish endpoint.

MyGet has the following feed endpoints available:
* /F/<your_feed_name> - the NuGet v2 API endpoint
* /F/<your_feed_name>/api/v2 - the NuGet v2 API endpoint for consuming packages
* /F/<your_feed_name>/api/v2/package - the NuGet v2 API endpoint for pushing packages
* /F/<your_feed_name>/api/v1 - the NuGet v1 API endpoint for consuming and pushing packages (still in use by Orchard CMS and some others)

The following table lists which endpoint can be used with which client:

<table>
    <thead>
        <tr>
            <th>Endpoint</th>
            <th>NuGet < v1.6</th>
            <th>NuGet > v1.6</th>
            <th>NuGet Package Explorer</th>
            <th>Orchard CMS</th>
            <th>Web browser</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>/F/<your_feed_name></td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/<your_feed_name>/api/v2</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/<your_feed_name>/api/v2/package</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>no</td>
		</tr>
        <tr>
            <td>/F/<your_feed_name>/api/v1</td>
            <td>yes</td>
            <td>yes (no push)</td>
            <td>yes</td>
            <td>yes</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/RSS/<your_feed_name></td>
            <td>no</td>
            <td>no</td>
            <td>no</td>
            <td>no</td>
            <td>yes</td>
		</tr>
    </tbody>
</table>