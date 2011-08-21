// Type: BoxSync.Core.BoxManager
// Assembly: BoxSync.Core, Version=0.3.2.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Dev\box-csharp\BoxSync.Core.dll

using BoxSync.Core.Primitives;
using BoxSync.Core.Statuses;
using System.Net;

namespace BoxSync.Core
{
    public sealed class BoxManager
    {
        public BoxManager(string applicationApiKey, string serviceUrl, IWebProxy proxy);
        public BoxManager(string applicationApiKey, string serviceUrl, IWebProxy proxy, string authorizationToken);
        public string AuthenticationToken { get; set; }
        public IWebProxy Proxy { get; }
        public AuthenticationStatus AuthenticateUser(string login, string password, string method, out string authenticationToken, out User authenticatedUser);
        public void AuthenticateUser(string login, string password, string method, OperationFinished<AuthenticateUserResponse> authenticateUserCompleted);
        public void AuthenticateUser(string login, string password, string method, OperationFinished<AuthenticateUserResponse> authenticateUserCompleted, object userState);
        public GetAuthenticationTokenStatus GetAuthenticationToken(string authenticationTicket, out string authenticationToken, out User authenticatedUser);
        public void GetAuthenticationToken(string authenticationTicket, OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted);
        public void GetAuthenticationToken(string authenticationTicket, OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted, object userState);
        public GetTicketStatus GetTicket(out string authenticationTicket);
        public void GetTicket(OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted);
        public void GetTicket(OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted, object userState);
        public UploadFileResponse AddFile(string filePath, long destinationFolderID);
        public void AddFile(string filePath, long destinationFolderID, OperationFinished<UploadFileResponse> fileUploadCompleted);
        public void AddFile(string filePath, long destinationFolderID, OperationFinished<UploadFileResponse> fileUploadCompleted, object userState);
        public UploadFileResponse AddFiles(string[] files, long destinationFolderID);
        public UploadFileResponse AddFiles(string[] files, long destinationFolderID, bool isFilesShared, string message, string[] emailsToNotify);
        public void AddFiles(string[] files, long destinationFolderID, bool isFilesShared, string message, string[] emailsToNotify, OperationFinished<UploadFileResponse> filesUploadCompleted);
        public void AddFiles(string[] files, long destinationFolderID, bool isFilesShared, string message, string[] emailsToNotify, OperationFinished<UploadFileResponse> filesUploadCompleted, object userState);
        public OverwriteFileResponse OverwriteFile(string filePath, long fileID);
        public OverwriteFileResponse OverwriteFile(string filePath, long fileID, bool isFileShared, string message, string[] emailsToNotify);
        public void OverwriteFile(string filePath, long fileID, OperationFinished<OverwriteFileResponse> overwriteFileCompleted);
        public void OverwriteFile(string filePath, long fileID, bool isFileShared, string message, string[] emailsToNotify, OperationFinished<OverwriteFileResponse> overwriteFileCompleted, object userState);
        public FileNewCopyResponse FileNewCopy(string filePath, long fileID);
        public FileNewCopyResponse FileNewCopy(string filePath, long fileID, bool isFileShared, string message, string[] emailsToNotify);
        public void FileNewCopy(string filePath, long fileID, OperationFinished<FileNewCopyResponse> fileNewCopyCompleted);
        public void FileNewCopy(string filePath, long fileID, bool isFileShared, string message, string[] emailsToNotify, OperationFinished<FileNewCopyResponse> fileNewCopyCompleted, object userState);
        public CreateFolderStatus CreateFolder(string folderName, long parentFolderID, bool isShared, out FolderBase folder);
        public void CreateFolder(string folderName, long parentFolderID, bool isShared, OperationFinished<CreateFolderResponse> createFolderCompleted);
        public void CreateFolder(string folderName, long parentFolderID, bool isShared, OperationFinished<CreateFolderResponse> createFolderCompleted, object userState);
        public DeleteObjectStatus DeleteObject(long objectID, ObjectType objectType);
        public void DeleteObject(long objectID, ObjectType objectType, OperationFinished<DeleteObjectResponse> deleteObjectCompleted);
        public void DeleteObject(long objectID, ObjectType objectType, OperationFinished<DeleteObjectResponse> deleteObjectCompleted, object userState);
        public GetAccountTreeStatus GetRootFolderStructure(RetrieveFolderStructureOptions retrieveOptions, out Folder folder);
        public void GetRootFolderStructure(RetrieveFolderStructureOptions retrieveOptions, OperationFinished<GetFolderStructureResponse> getFolderStructureCompleted);
        public void GetRootFolderStructure(RetrieveFolderStructureOptions retrieveOptions, OperationFinished<GetFolderStructureResponse> getFolderStructureCompleted, object userState);
        public GetAccountTreeStatus GetFolderStructure(long folderID, RetrieveFolderStructureOptions retrieveOptions, out Folder folder);
        public void GetFolderStructure(long folderID, RetrieveFolderStructureOptions retrieveOptions, OperationFinished<GetFolderStructureResponse> getFolderStructureCompleted);
        public void GetFolderStructure(long folderID, RetrieveFolderStructureOptions retrieveOptions, OperationFinished<GetFolderStructureResponse> getFolderStructureCompleted, object userState);
        public ExportTagsStatus ExportTags(out TagPrimitiveCollection tagList);
        public void ExportTags(OperationFinished<ExportTagsResponse> exportTagsCompleted);
        public void ExportTags(OperationFinished<ExportTagsResponse> exportTagsCompleted, object userState);
        public SetDescriptionStatus SetDescription(long objectID, ObjectType objectType, string description);
        public void SetDescription(long objectID, ObjectType objectType, string description, OperationFinished<SetDescriptionResponse> setDescriptionCompleted);
        public void SetDescription(long objectID, ObjectType objectType, string description, OperationFinished<SetDescriptionResponse> setDescriptionCompleted, object userState);
        public RenameObjectStatus RenameObject(long objectID, ObjectType objectType, string newName);
        public void RenameObject(long objectID, ObjectType objectType, string newName, OperationFinished<RenameObjectResponse> renameObjectCompleted);
        public void RenameObject(long objectID, ObjectType objectType, string newName, OperationFinished<RenameObjectResponse> renameObjectCompleted, object userState);
        public MoveObjectStatus MoveObject(long targetObjectID, ObjectType targetObjectType, long destinationFolderID);
        public void MoveObject(long targetObjectID, ObjectType targetObjectType, long destinationFolderID, OperationFinished<MoveObjectResponse> moveObjectCompleted);
        public void MoveObject(long targetObjectID, ObjectType targetObjectType, long destinationFolderID, OperationFinished<MoveObjectResponse> moveObjectCompleted, object userState);
        public LogoutStatus Logout();
        public void Logout(OperationFinished<LogoutResponse> logoutCompleted);
        public void Logout(OperationFinished<LogoutResponse> logoutCompleted, object userState);
        public RegisterNewUserStatus RegisterNewUser(string login, string password, out RegisterNewUserResponse response);
        public void RegisterNewUser(string login, string password, OperationFinished<RegisterNewUserResponse> registerNewUserCompleted);
        public void RegisterNewUser(string login, string password, OperationFinished<RegisterNewUserResponse> registerNewUserCompleted, object userState);
        public VerifyRegistrationEmailStatus VerifyRegistrationEmail(string login);
        public void VerifyRegistrationEmail(string login, OperationFinished<VerifyRegistrationEmailResponse> verifyRegistrationEmailCompleted);
        public void VerifyRegistrationEmail(string login, OperationFinished<VerifyRegistrationEmailResponse> verifyRegistrationEmailCompleted, object userState);
        public AddToMyBoxStatus AddToMyBox(long targetFileID, long destinationFolderID, TagPrimitiveCollection tagList);
        public AddToMyBoxStatus AddToMyBox(string targetFileName, long destinationFolderID, TagPrimitiveCollection tagList);
        public void AddToMyBox(long targetFileID, long destinationFolderID, TagPrimitiveCollection tagList, OperationFinished<AddToMyBoxResponse> addToMyBoxCompleted);
        public void AddToMyBox(long targetFileID, long destinationFolderID, TagPrimitiveCollection tagList, OperationFinished<AddToMyBoxResponse> addToMyBoxCompleted, object userState);
        public void AddToMyBox(string targetFileName, long destinationFolderID, TagPrimitiveCollection tagList, OperationFinished<AddToMyBoxResponse> addToMyBoxCompleted);
        public void AddToMyBox(string targetFileName, long destinationFolderID, TagPrimitiveCollection tagList, OperationFinished<AddToMyBoxResponse> addToMyBoxCompleted, object userState);
        public PublicShareStatus PublicShare(long targetObjectID, ObjectType targetObjectType, string password, string notificationMessage, string[] emailList, out string publicName);
        public void PublicShare(long targetObjectID, ObjectType targetObjectType, string password, string message, string[] emailList, bool sendNotification, OperationFinished<PublicShareResponse> publicShareCompleted);
        public void PublicShare(long targetObjectID, ObjectType targetObjectType, string password, string message, string[] emailList, bool sendNotification, OperationFinished<PublicShareResponse> publicShareCompleted, object userState);
        public PublicUnshareStatus PublicUnshare(long targetObjectID, ObjectType targetObjectType);
        public void PublicUnshare(long targetObjectID, ObjectType targetObjectType, OperationFinished<PublicUnshareResponse> publicUnshareCompleted);
        public void PublicUnshare(long targetObjectID, ObjectType targetObjectType, OperationFinished<PublicUnshareResponse> publicUnshareCompleted, object userState);
        public PrivateShareStatus PrivateShare(long targetObjectID, ObjectType targetObjectType, string password, string notificationMessage, string[] emailList, bool sendNotification);
        public void PrivateShare(long targetObjectID, ObjectType targetObjectType, string password, string notificationMessage, string[] emailList, bool sendNotification, OperationFinished<PrivateShareResponse> privateShareCompleted);
        public void PrivateShare(long targetObjectID, ObjectType targetObjectType, string password, string notificationMessage, string[] emailList, bool sendNotification, OperationFinished<PrivateShareResponse> privateShareCompleted, object userState);
    }
}
