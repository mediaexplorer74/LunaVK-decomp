// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.VKErrors
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Network
{
  public enum VKErrors
  {
    None = 0,
    UnknownError = 1,
    ApplicationIsDisabled = 2,
    UnknownMethod = 3,
    IncorrectSignature = 4,
    AuthorizationFailed = 5,
    TooManyRequests = 6,
    PermissionIsDenied = 7,
    InvalidRequest = 8,
    FloodControl = 9,
    InternalServerError = 10, // 0x0000000A
    InTestModeAppShouldBeDisabled = 11, // 0x0000000B
    CaptchaNeeded = 14, // 0x0000000E
    AccessDenied = 15, // 0x0000000F
    HTTPAuthorizationFailed = 16, // 0x00000010
    ValidationRequed = 17, // 0x00000011
    ContentUnavailable = 19, // 0x00000013
    PermissionDeniedForNonStandaloneApps = 20, // 0x00000014
    PermissionAllowedOnlyForStandaloneAndOpenAPIApps = 21, // 0x00000015
    DownloadError = 22, // 0x00000016
    MethodDisabled = 23, // 0x00000017
    InvalidOrMissingParameter = 100, // 0x00000064
    InvalidAppAPIID = 101, // 0x00000065
    OutOfLimits = 103, // 0x00000067
    CantSaveFile = 105, // 0x00000069
    InvalidUserID = 113, // 0x00000071
    InvalidHash = 121, // 0x00000079
    InvalidAudioFormat = 123, // 0x0000007B
    InvalidTimestamp = 150, // 0x00000096
    InvalidListID = 171, // 0x000000AB
    MaxListsCount = 173, // 0x000000AD
    CantAddYourself = 174, // 0x000000AE
    CantAddIfYouBlocked = 175, // 0x000000AF
    CantAddABlocked = 176, // 0x000000B0
    AccessToAlbumDenied = 200, // 0x000000C8
    AccessToAudioDenied = 201, // 0x000000C9
    AccessToVideoDenied = 202, // 0x000000CA
    AccessToGroupDenied = 203, // 0x000000CB
    AccessVideoSaveDenied = 204, // 0x000000CC
    AccessToAddingPostDenied = 214, // 0x000000D6
    AdvertisingPostHasRecentlyAdded = 219, // 0x000000DB
    UserDisableTrackNameBroadcast = 221, // 0x000000DD
    NoAccessToPoll = 250, // 0x000000FA
    PollIsInvalid = 251, // 0x000000FB
    ResponseIsInvalid = 252, // 0x000000FC
    AudioIsProhibited = 270, // 0x0000010E
    AlbumIsFull = 300, // 0x0000012C
    InvalidFileName = 301, // 0x0000012D
    MaxAlbumsCountOrInvalidFileSize = 302, // 0x0000012E
    PermissionToVotesDenied = 500, // 0x000001F4
    PermissionToAccessObjectsDenied = 600, // 0x00000258
    SomeAdsError = 603, // 0x0000025B
    CredentialsError = 700, // 0x000002BC
    UserMustBeInCommunity = 701, // 0x000002BD
    LeadersLimitReached = 702, // 0x000002BE
    VideoHasAlreadyBeenAdded = 800, // 0x00000320
    WrongDocumentID = 1150, // 0x0000047E
    AccessToDocumentDenied = 1151, // 0x0000047F
    PhotoOriginalWasChanged = 1160, // 0x00000488
    ConnectionError = 10000, // 0x00002710
    CaptchaCanceled = 10001, // 0x00002711
    NoNetwork = 90909, // 0x0001631D
  }
}
