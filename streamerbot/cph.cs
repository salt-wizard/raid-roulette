using Streamer.bot.Common.Events;
using Twitch.Common.Models.Api;
using EventSource = Streamer.bot.Common.Events.EventSource;
using Streamer.bot.Plugin.Interface.Model;

public static class CPH
{
    public static void StreamDeckSetBackgroundLocal(string buttonId, string imageFile, string color, int state){}
    public static void StreamDeckSetTitle(string buttonId, string title){}
    public static void StreamDeckSetTitle(string buttonId, string title, int state){}
    public static void StreamDeckSetState(string buttonId, int state){}
    public static void StreamDeckSetValue(string buttonId, string value){}
    public static void StreamDeckShowAlert(string buttonId){}
    public static void StreamDeckShowOk(string buttonId){}
    public static void StreamDeckToggleState(string buttonId){}
    public static Twitch.Common.Models.Api.GuestStarSettings? TwitchGetChannelGuestStarSettings(){ return null;}
    public static bool TwitchUpdateChannelGuestStarSettings(Boolean? isModeratorSendLiveEnabled, Int32? slotCount, Boolean? isBrowserSourceAudioEnabled, string groupLayout, Boolean? regeneratgeBrowserSource){return false;}
    public static Twitch.Common.Models.Api.GuestSession? TwitchGetGuestStarSession(){return null;}
    public static Twitch.Common.Models.Api.GuestSession? TwitchCreateGuestStarSession(){return null;}
    public static Twitch.Common.Models.Api.GuestSession? TwitchEndGuestStarSession(){return null;}
    public static List<Twitch.Common.Models.Api.GuestStarInvite>? TwitchGetGuestStarInvites(){return null;}
    public static bool TwitchSendGuestStarInvite(string userLogin){return false;}
    public static bool TwitchDeleteGuestStarInvite(string userLogin){return false;}
    public static bool TwitchAssignGuestStarSlot(string userLogin, int slot){return false;}
    public static bool TwitchUpdateGuestStarSlot(int sourceSlot, int destinationSlot){return false;}
    public static bool TwitchDeleteGuestStarSlot(string userLogin, int slot){return false;}
    public static bool TwitchUpdateGuestStarSlotSettings(int slotId, Boolean? isAudioEnabled, Boolean? isVideoEnabled, Boolean? isLive, Int32? volume){return false;}
    public static bool RegisterCustomTrigger(string triggerName, string eventName, System.String[] categories){return false;}
    public static void TriggerEvent(string eventName, bool useArgs){}
    public static void TriggerCodeEvent(string eventName, bool useArgs){}
    public static void TriggerCodeEvent(string eventName, Dictionary<string,object> args){}
    public static bool VTubeStudioLoadModelById(string modelId){return false;}
    public static bool VTubeStudioLoadModelByName(string modelName){return false;}
    public static bool VTubeStudioTriggerHotkeyById(string hotkeyId){return false;}
    public static bool VTubeStudioTriggerHotkeyByName(string hotkeyName){return false;}
    public static bool VTubeStudioMoveModel(System.Double seconds, bool relative, Double? posX, Double? posY, Double? rotation, Double? size){return false;}
    public static bool VTubeStudioRandomColorTint(){return false;}
    public static bool VTubeStudioResetAllColorTints(){return false;}
    public static bool VTubeStudioColorTintAll(string hexColor, System.Double mixWithSceneLighting){return false;}
    public static bool VTubeStudioColorTintByNumber(string hexColor, System.Double mixWithSceneLighting, List<int> artMeshNumbers){return false;}
    public static bool VTubeStudioColorTintByNames(string hexColor, System.Double mixWithSceneLighting, List<string> filterValues){return false;}
    public static bool VTubeStudioColorTintByNameContains(string hexColor, System.Double mixWithSceneLighting, List<string> filterValues){return false;}
    public static bool VTubeStudioColorTintByTags(string hexColor, System.Double mixWithSceneLighting, List<string> filterValues){return false;}
    public static bool VTubeStudioColorTintByTagContains(string hexColor, System.Double mixWithSceneLighting, List<string> filterValues){return false;}
    public static bool VTubeStudioActivateExpression(string expressionFile){return false;}
    public static bool VTubeStudioDeactivateExpression(string expressionFile){return false;}
    public static string VTubeStudioSendRawRequest(string requestType, string data){return "";}
    public static Streamer.bot.Plugin.Interface.VTSModelPosition? VTubeStudioGetModelPosition(){return null;}
    public static void ShowToastNotification(string title, string message, string attribution, string iconPath){}
    public static void ShowToastNotification(string id, string title, string message, string attribution, string iconPath){}
    public static void WaveLinkOutputMute(string mixer){}
    public static void WaveLinkOutputUnmute(string mixer){}
    public static void WaveLinkOutputToggleMute(string mixer){}
    public static void WaveLinkSetOutputVolume(string mixer, int volume){}
    public static string WaveLinkGetMicrophoneIdentifier(string microphoneName){return "";}
    public static void WaveLinkMicrophoneMute(string microphoneIdentifier){}
    public static void WaveLinkMicrophoneUnmute(string microphoneIdentifier){}
    public static void WaveLinkMicrophoneToggleMute(string microphoneIdentifier){}
    public static void WaveLinkMicrophoneSetVolume(string microphoneIdentifier, System.Double volume){}
    public static System.Double WaveLinkMicrophoneGetVolume(string microphoneIdentifier){return new System.Double();}
    public static string WaveLinkGetInputIdentifier(string inputName){return "";}
    public static void WaveLinkInputMute(string identifier, string mixer){}
    public static void WaveLinkInputUnmute(string identifier, string mixer){}
    public static void WaveLinkInputToggleMute(string identifier, string mixer){}
    public static void WaveLinkInputSetVolume(string inputIdentifier, string mixer, int volume){}
    public static long WaveLinkInputGetVolume(string inputIdentifier, string mixer){return 0L;}
    public static void WaveLinkInputFilterBypassBypassed(string inputIdentifier, string mixer){}
    public static void WaveLinkInputFilterBypassEnabled(string inputIdentifier, string mixer){}
    public static void WaveLinkInputFilterBypassToggle(string inputIdentifier, string mixer){}
    public static string WaveLinkInputGetFilterIdentifier(string inputIdentifier, string filterName){return "";}
    public static void WaveLinkInputFilterEnable(string inputIdentifier, string filterIdentifier){}
    public static void WaveLinkInputFilterDisable(string inputIdentifier, string filterIdentifier){}
    public static void WaveLinkInputFilterToggle(string inputIdentifier, string filterIdentifier){}
    public static bool WaveLinkInputGetFilterState(string inputIdentifier, string filterIdentifier){return false;}
    public static Streamer.bot.Plugin.Interface.Model.QuoteData? GetQuote(int quoteId){return null;}
    public static int GetQuoteCount(){return 0;}
    public static int AddQuoteForTwitch(string userId, string quote, bool captureGame){return 0;}
    public static int AddQuoteForYouTube(string userId, string quote){return 0;}
    public static bool DeleteQuote(int quoteId){return false;}
    public static void WebsocketConnect(int connection){}
    public static void WebsocketDisconnect(int connection){}
    public static bool WebsocketIsConnected(int connection){return false;}
    public static void WebsocketSend(string data, int connection){}
    public static void WebsocketSend(System.Byte[] data, int connection){}
    public static void WebsocketBroadcastString(string data){}
    public static void WebsocketBroadcastJson(string data){}
    public static void AddToCredits(string section, string value, bool json){}
    public static void ResetCredits(){}
    public static bool ExecuteMethod(string executeCode, string methodName){return false;}
    public static void PauseActionQueue(string name){}
    public static void PauseAllActionQueues(){}
    public static void ResumeActionQueue(string name, bool clear){}
    public static void ResumeAllActionQueues(bool clear){}
    public static void ResetFirstWords(){}
    public static int WebsocketCustomServerGetConnectionByName(string name){return 0;}
    public static void WebsocketCustomServerStart(int connection){}
    public static void WebsocketCustomServerStop(int connection){}
    public static bool WebsocketCustomServerIsListening(int connection){return false;}
    public static void WebsocketCustomServerCloseAllSessions(int connection){}
    public static void WebsocketCustomServerCloseSession(string sessionId, int connection){}
    public static void WebsocketCustomServerBroadcast(string data, string sessionId, int connection){}
    public static bool TwitchPollCreate(string title, List<string> choices, int duration, int channelPointsPerVote){return false;}
    public static void TwitchPollTerminate(string pollId){}
    public static void TwitchPollArchive(string pollId){}
    public static void TwitchSubscriberOnly(bool enabled){}
    public static void TwitchEmoteOnly(bool enabled){}
    public static void TwitchSlowMode(bool enabled, int duration){}
    public static void TwitchFollowMode(bool enabled, int duration){}
    public static string TwitchPredictionCreate(string title, string firstOption, string secondOption, int duration){return "";}
    public static string TwitchPredictionCreate(string title, List<string> options, int duration){return "";}
    public static void TwitchPredictionCancel(string predictionId){}
    public static void TwitchPredictionLock(string predictionId){}
    public static void TwitchPredictionResolve(string predictionId, string winningId){}
    public static bool TwitchRunCommercial(int duration){return false;}
    public static void TwitchAnnounce(string message, bool bot, string color){}
    public static bool TwitchStartRaidById(string userId){return false;}
    public static bool TwitchStartRaidByName(string userName){return false;}
    public static bool TwitchCancelRaid(){return false;}
    public static bool TwitchSendShoutoutById(string userId){return false;}
    public static bool TwitchSendShoutoutByLogin(string userLogin){return false;}
    public static void EnableCommand(string id){}
    public static void DisableCommand(string id){}
    public static void CommandResetGlobalCooldown(string id){}
    public static void CommandRemoveGlobalCooldown(string id){}
    public static void CommandAddToGlobalCooldown(string id, int seconds){}
    public static void CommandResetUserCooldown(string id, int userId){}
    public static void CommandResetUserCooldown(string id, string userId){}
    public static void CommandRemoveUserCooldown(string id, int userId){}
    public static void CommandRemoveUserCooldown(string id, string userId){}
    public static void CommandAddToUserCooldown(string id, int userId, int seconds){}
    public static void CommandAddToUserCooldown(string id, string userId, int seconds){}
    public static void CommandResetAllUserCooldowns(string id){}
    public static void CommandRemoveAllUserCooldowns(string id){}
    public static void CommandAddToAllUserCooldowns(string id, int seconds){}
    public static void CommandSetGlobalCooldownDuration(string id, int seconds){}
    public static void CommandSetUserCooldownDuration(string id, int seconds){}
    public static int TtsSpeak(string voiceAlias, string message, bool badWordFilter){return 0;}
    public static void KeyboardPress(string keyPress){}
    public static void SendYouTubeMessage(string message, bool bot){}
    public static void VoiceModSelectVoice(string voiceId){}
    public static string VoiceModGetCurrentVoice(){return "";}
    public static bool VoiceModGetHearMyselfStatus(){return false;}
    public static void VoiceModHearMyVoiceOn(){}
    public static void VoiceModHearMyVoiceOff(){}
    public static bool VoiceModGetVoiceChangerStatus(){return false;}
    public static void VoiceModVoiceChangerOn(){}
    public static void VoiceModVoiceChangerOff(){}
    public static void VoiceModCensorOn(){}
    public static void VoiceModCensorOff(){}
    public static bool VoiceModGetBackgroundEffectStatus(){return false;}
    public static void VoiceModBackgroundEffectOn(){}
    public static void VoiceModBackgroundEffectOff(){}
    public static void LumiaSendCommand(string command){}
    public static void LumiaSetToDefault(){}
    public static bool DiscordPostTextToWebhook(string webhookUrl, string content, string username, string avatarUrl, bool textToSpeech){return false;}
    public static void StreamDeckSetBackgroundColor(string buttonId, string color){}
    public static void StreamDeckSetBackgroundColor(string buttonId, string color, int state){}
    public static void StreamDeckSetBackgroundUrl(string buttonId, string imageUrl){}
    public static void StreamDeckSetBackgroundUrl(string buttonId, string imageUrl, string color){}
    public static void StreamDeckSetBackgroundUrl(string buttonId, string imageUrl, int state){}
    public static void StreamDeckSetBackgroundUrl(string buttonId, string imageUrl, string color, int state){}
    public static void StreamDeckSetBackgroundLocal(string buttonId, string imageFile){}
    public static void StreamDeckSetBackgroundLocal(string buttonId, string imageFile, string color){}
    public static void StreamDeckSetBackgroundLocal(string buttonId, string imageFile, int state){}
    public static void ObsReplayBufferSave(int connection){}
    public static void ObsSetMediaSourceFile(string scene, string source, string file, int connection){}
    public static void ObsSetImageSourceFile(string scene, string source, string file, int connection){}
    public static string ObsGetSceneItemProperties(string scene, string source, int connection){return "";}
    public static void ObsHideSceneSources(string scene, int connection){}
    public static string ObsSetRandomSceneSourceVisible(string scene, int connection){return "";}
    public static bool ObsTakeScreenshot(string source, string path, int quality, int connection){return false;}
    public static void ObsSetColorSourceColor(string scene, string source, int a, int r, int g, int b, int connection){}
    public static void ObsSetColorSourceColor(string scene, string source, string hexColor, int connection){}
    public static void ObsSetColorSourceRandomColor(string scene, string source, int connection){}
    public static bool SlobsIsConnected(int connection){return false;}
    public static bool SlobsConnect(int connection){return false;}
    public static void SlobsDisconnect(int connection){}
    public static bool SlobsIsStreaming(int connection){return false;}
    public static void SlobsStopStreaming(int connection){}
    public static void SlobsStartStreaming(int connection){}
    public static bool SlobsIsRecording(int connection){return false;}
    public static void SlobsStartRecording(int connection){}
    public static void SlobsStopRecording(int connection){}
    public static void SlobsPauseRecording(int connection){}
    public static void SlobsResumeRecording(int connection){}
    public static void SlobsSetScene(string sceneName, int connection){}
    public static string SlobsGetCurrentScene(int connection){return "";}
    public static bool SlobsIsSourceVisible(string scene, string source, int connection){return false;}
    public static void SlobsSetSourceVisibility(string scene, string source, bool visible, int connection){}
    public static void SlobsSetSourceVisibilityState(string scene, string source, int state, int connection){}
    public static void SlobsShowSource(string scene, string source, int connection){}
    public static void SlobsHideSource(string scene, string source, int connection){}
    public static void SlobsHideGroupsSources(string scene, string groupName, int connection){}
    public static string SlobsSetRandomGroupSourceVisible(string scene, string groupName, int connection){return "";}
    public static List<string> SlobsGetGroupSources(string scene, string groupName, int connection){return new List<string>();}
    public static void SlobsSetBrowserSource(string scene, string source, string url, int connection){}
    public static void SlobsSetGdiText(string scene, string source, string text, int connection){}
    public static bool SlobsIsFilterEnabled(string scene, string filterName, int connection){return false;}
    public static bool SlobsIsFilterEnabled(string scene, string source, string filterName, int connection){return false;}
    public static void SlobsSetFilterState(string scene, string filterName, int state, int connection){}
    public static void SlobsSetFilterState(string scene, string source, string filterName, int state, int connection){}
    public static void SlobsShowFilter(string scene, string filterName, int connection){}
    public static void SlobsShowFilter(string scene, string source, string filterName, int connection){}
    public static void SlobsHideFilter(string scene, string filterName, int connection){}
    public static void SlobsHideFilter(string scene, string source, string filterName, int connection){}
    public static void SlobsToggleFilter(string scene, string filterName, int connection){}
    public static void SlobsToggleFilter(string scene, string source, string filterName, int connection){}
    public static void SlobsSetRandomFilterState(string scene, int state, int connection){}
    public static void SlobsSetRandomFilterState(string scene, string source, int state, int connection){}
    public static void SlobsSetSourceMuteState(string scene, string source, int state, int connection){}
    public static void SlobsSourceMute(string scene, string source, string filterName, int connection){}
    public static void SlobsSourceUnMute(string scene, string source, string filterName, int connection){}
    public static void SlobsSourceMuteToggle(string scene, string source, string filterName, int connection){}
    public static void ClearNonPersistedGlobals(){}
    public static void ClearNonPersistedUserGlobals(){}
    public static T? GetGlobalVar<T>(string varName, bool persisted){return default;}
    public static List<Streamer.bot.Plugin.Interface.Model.GlobalVariableValue> GetGlobalVarValues(bool persisted){return new List<Streamer.bot.Plugin.Interface.Model.GlobalVariableValue>();}
    public static void SetGlobalVar(string varName, object value, bool persisted){}
    public static void UnsetGlobalVar(string varName, bool persisted){}
    public static T? GetUserVar<T>(string userName, string varName, bool persisted){return default;}
    public static List<UserVariableValue<T>> GetTwitchUsersVar<T>(string varName, bool persisted){return new List<UserVariableValue<T>>();}
    public static List<UserVariableValue<T>> GetYouTubeUsersVar<T>(string varName, bool persisted){return new List<UserVariableValue<T>>();}
    public static T? GetTwitchUserVarById<T>(string userId, string varName, bool persisted){return default;}
    public static T? GetYouTubeUserVarById<T>(string userId, string varName, bool persisted){return default;}
    public static T? GetTwitchUserVar<T>(string userName, string varName, bool persisted){return default;}
    public static T? GetYouTubeUserVar<T>(string userName, string varName, bool persisted){return default;}
    public static void SetUserVar(string userName, string varName, object value, bool persisted){}
    public static void SetTwitchUserVarById(string userId, string varName, object value, bool persisted){}
    public static void SetYouTubeUserVarById(string userId, string varName, object value, bool persisted){}
    public static void SetTwitchUsersVarById(List<string> userIds, string varName, object value, bool persisted){}
    public static void SetYouTubeUsersVarById(List<string> userIds, string varName, object value, bool persisted){}
    public static void SetTwitchUserVar(string userName, string varName, object value, bool persisted){}
    public static void SetYouTubeUserVar(string userName, string varName, object value, bool persisted){}
    public static void UnsetUserVar(string userName, string varName, bool persisted){}
    public static void UnsetTwitchUserVarById(string userId, string varName, bool persisted){}
    public static void UnsetYouTubeUserVarById(string userId, string varName, bool persisted){}
    public static void UnsetTwitchUserVar(string userName, string varName, bool persisted){}
    public static void UnsetYouTubeUserVar(string userName, string varName, bool persisted){}
    public static void UnsetUser(string userName, bool persisted){}
    public static void UnsetTwitchUserById(string userId, bool persisted){}
    public static void UnsetYouTubeUserById(string userId, bool persisted){}
    public static void UnsetTwitchUser(string userName, bool persisted){}
    public static void UnsetYouTubeUser(string userName, bool persisted){}
    public static void UnsetAllUsersVar(string varName, bool persisted){}
    public static void DisableReward(string rewardId){}
    public static void EnableReward(string rewardId){}
    public static void PauseReward(string rewardId){}
    public static void UnPauseReward(string rewardId){}
    public static void UpdateRewardCost(string rewardId, int cost, bool additive){}
    public static void UpdateRewardCooldown(string rewardId, long cooldown, bool additive){}
    public static bool UpdateRewardTitle(string rewardId, string title){return false;}
    public static bool UpdateRewardPrompt(string rewardId, string prompt){return false;}
    public static bool UpdateRewardBackgroundColor(string rewardId, string backgroundColor){return false;}
    public static bool UpdateReward(string rewardId, string title, string prompt, Int32? cost, string backroundColor){return false;}
    public static bool TwitchRedemptionFulfill(string rewardId, string redemptionId){return false;}
    public static bool TwitchRedemptionCancel(string rewardId, string redemptionId){return false;}
    public static long TwitchGetChannelPointsUsedByUserId(string userId){return 0L;}
    public static void TwitchResetRewardCounter(string rewardId){}
    public static void TwitchResetRewardUserCounters(string rewardId){}
    public static void TwitchResetUserRewardCounters(string userId, bool persisted){}
    public static void TwitchResetUserRewardCounter(string rewardId, string userId){}
    public static void TwitchRewardGroupEnable(string groupName){}
    public static void TwitchRewardGroupDisable(string groupName){}
    public static void TwitchRewardGroupToggleEnable(string groupName){}
    public static void TwitchRewardGroupPause(string groupName){}
    public static void TwitchRewardGroupUnPause(string groupName){}
    public static void TwitchRewardGroupTogglePause(string groupName){}
    public static List<Streamer.bot.Plugin.Interface.TwitchReward> TwitchGetRewards(){return new List<Streamer.bot.Plugin.Interface.TwitchReward>();}
    public static long TwitchGetBitsDonatedByUserId(string userId){return 0L;}
    public static bool TwitchIsUserSubscribed(string userId, out String tier){tier = ""; return false;}
    public static void PlaySound(string fileName, System.Single volume, bool finishBeforeContinuing){}
    public static void PlaySoundFromFolder(string path, System.Single volume, bool recursive, bool finishBeforeContinuing){}
    public static int BroadcastUdp(int port, object data){return 0;}
    public static int ObsGetConnectionByName(string name){return 0;}
    public static long ObsConvertRgb(int a, int r, int g, int b){return 0L;}
    public static long ObsConvertColorHex(string colorHex){return 0L;}
    public static bool ObsIsConnected(int connection){return false;}
    public static bool ObsConnect(int connection){return false;}
    public static void ObsDisconnect(int connection){}
    public static bool ObsIsStreaming(int connection){return false;}
    public static void ObsStopStreaming(int connection){}
    public static void ObsStartStreaming(int connection){}
    public static bool ObsIsRecording(int connection){return false;}
    public static void ObsStartRecording(int connection){}
    public static void ObsStopRecording(int connection){}
    public static void ObsPauseRecording(int connection){}
    public static void ObsResumeRecording(int connection){}
    public static void ObsSetScene(string sceneName, int connection){}
    public static string ObsGetCurrentScene(int connection){return "";}
    public static bool ObsIsSourceVisible(string scene, string source, int connection){return false;}
    public static void ObsSetSourceVisibility(string scene, string source, bool visible, int connection){}
    public static void ObsSetSourceVisibilityState(string scene, string source, int state, int connection){}
    public static void ObsShowSource(string scene, string source, int connection){}
    public static void ObsHideSource(string scene, string source, int connection){}
    public static void ObsHideGroupsSources(string scene, string groupName, int connection){}
    public static string ObsSetRandomGroupSourceVisible(string scene, string groupName, int connection){return "";}
    public static List<string> ObsGetGroupSources(string scene, string groupName, int connection){return new List<string>();}
    public static void ObsSetBrowserSource(string scene, string source, string url, int connection){}
    public static void ObsSetGdiText(string scene, string source, string text, int connection){}
    public static bool ObsIsFilterEnabled(string scene, string filterName, int connection){return false;}
    public static bool ObsIsFilterEnabled(string scene, string source, string filterName, int connection){return false;}
    public static void ObsSetFilterState(string scene, string filterName, int state, int connection){}
    public static void ObsSetFilterState(string scene, string source, string filterName, int state, int connection){}
    public static void ObsShowFilter(string scene, string filterName, int connection){}
    public static void ObsShowFilter(string scene, string source, string filterName, int connection){}
    public static void ObsHideFilter(string scene, string filterName, int connection){}
    public static void ObsHideFilter(string scene, string source, string filterName, int connection){}
    public static void ObsHideSourcesFilters(string scene, string source, int connection){}
    public static void ObsHideScenesFilters(string scene, int connection){}
    public static void ObsToggleFilter(string scene, string filterName, int connection){}
    public static void ObsToggleFilter(string scene, string source, string filterName, int connection){}
    public static void ObsSetRandomFilterState(string scene, int state, int connection){}
    public static void ObsSetRandomFilterState(string scene, string source, int state, int connection){}
    public static void ObsSetSourceMuteState(string scene, string source, int state, int connection){}
    public static void ObsSourceMute(string scene, string source, int connection){}
    public static void ObsSourceUnMute(string scene, string source, int connection){}
    public static void ObsSourceMuteToggle(string scene, string source, int connection){}
    public static string ObsSendRaw(string requestType, string data, int connection){return "";}
    public static string ObsSendBatchRaw(string data, bool haltOnFailure, int executionType, int connection){return "";}
    public static void ObsSetMediaState(string scene, string source, int state, int connection){}
    public static void ObsMediaPlay(string scene, string source, int connection){}
    public static void ObsMediaPause(string scene, string source, int connection){}
    public static void ObsMediaRestart(string scene, string source, int connection){}
    public static void ObsMediaStop(string scene, string source, int connection){}
    public static void ObsMediaNext(string scene, string source, int connection){}
    public static void ObsMediaPrevious(string scene, string source, int connection){}
    public static void ObsSetReplayBufferState(int state, int connection){}
    public static void ObsReplayBufferStart(int connection){}
    public static void ObsReplayBufferStop(int connection){}
    public static string get_TwitchClientId(){return "";}
    public static string get_TwitchOAuthToken(){return "";}
    public static int Between(int min, int max){return 0;}
    public static System.Double NextDouble(){return new System.Double();}
    public static void Wait(int milliseconds){}
    public static string UrlEncode(string text){return "";}
    public static string EscapeString(string text){return "";}
    public static bool UserIdInGroup(string userId, string groupName){return false;}
    public static bool UserInGroup(string userName, string groupName){return false;}
    public static bool AddUserIdToGroup(string userId, string groupName){return false;}
    public static bool AddUserToGroup(string userName, string groupName){return false;}
    public static bool RemoveUserIdFromGroup(string userId, string groupName){return false;}
    public static bool RemoveUserFromGroup(string userName, string groupName){return false;}
    public static bool ClearUsersFromGroup(string groupName){return false;}
    public static void SetArgument(string variableName, object value){}
    public static Streamer.bot.Common.Events.EventSource GetSource(){return EventSource.General;}
    public static Streamer.bot.Common.Events.EventType GetEventType(){return EventType.CommandTriggered;}
    public static bool TryGetArg(string argName, out Object? value){ value = null; return false;}
    public static bool TryGetArg<T>(string argName, out T? value){value = default; return false;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfo? TwitchGetBroadcaster(){return null;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfo? TwitchGetBot(){return null;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfo? TwitchGetUserInfoById(string userId){return null;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfo? TwitchGetUserInfoByLogin(string userLogin){return null;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfoEx? TwitchGetExtendedUserInfoById(string userId){return null;}
    public static Streamer.bot.Plugin.Interface.Model.TwitchUserInfoEx? TwitchGetExtendedUserInfoByLogin(string userLogin){return null;}
    public static void SendMessage(string message, bool bot){}
    public static void TwitchReplyToMessage(string message, string replyId, bool bot){}
    public static void SendAction(string action, bool bot){}
    public static bool SendWhisper(string userName, string message, bool bot){return false;}
    public static List<Twitch.Common.Models.Api.ClipData> GetAllClips(){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClips(int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId, DateTime start, DateTime end){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId, DateTime start, DateTime end, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId, System.TimeSpan duration){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(int userId, System.TimeSpan duration, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string username){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string userName, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string username, DateTime start, DateTime end){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string username, DateTime start, DateTime end, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string username, System.TimeSpan duration){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForUser(string username, System.TimeSpan duration, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId, DateTime start, DateTime end){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId, DateTime start, DateTime end, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId, System.TimeSpan duration){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.ClipData> GetClipsForGame(int gameId, System.TimeSpan duration, int count){return new List<ClipData>();}
    public static List<Twitch.Common.Models.Api.TeamInfo> GetTeamInfo(int userId){return new List<TeamInfo>();}
    public static List<Twitch.Common.Models.Api.TeamInfo> GetTeamInfo(string username){return new List<TeamInfo>();}
    public static List<Twitch.Common.Models.Api.TeamInfo> GetTeamInfoById(string userId){return new List<TeamInfo>();}
    public static List<Twitch.Common.Models.Api.TeamInfo> GetTeamInfoByLogin(string userLogin){return new List<TeamInfo>();}
    public static List<Twitch.Common.Models.Api.Cheermote> GetCheermotes(){return new List<Cheermote>();}
    public static bool SetChannelTitle(string title){return false;}
    public static Streamer.bot.Plugin.Interface.GameInfo? SetChannelGame(string game){return null;}
    public static bool SetChannelGameById(string gameId){return false;}
    public static bool TwitchClearChannelTags(){return false;}
    public static bool TwitchSetChannelTags(List<string> tags){return false;}
    public static bool TwitchAddChannelTag(string tag){return false;}
    public static bool TwitchRemoveChannelTag(string tag){return false;}
    public static Twitch.Common.Models.Api.ClipData? CreateClip(){return null;}
    public static Twitch.Common.Models.Api.StreamMarker? CreateStreamMarker(string description){return null;}
    public static bool TwitchAddModerator(string userName){return false;}
    public static bool TwitchRemoveModerator(string userName){return false;}
    public static bool TwitchAddVip(string userName){return false;}
    public static bool TwitchRemoveVip(string userName){return false;}
    public static bool TwitchBanUser(string userName, string reason, bool bot){return false;}
    public static bool TwitchUnbanUser(string userName, bool bot){return false;}
    public static bool TwitchTimeoutUser(string username, int duration, string reason, bool bot){return false;}
    public static bool TwitchClearChatMessages(bool bot){return false;}
    public static bool TwitchDeleteChatMessage(string messageId, bool bot){return false;}
    public static bool RunAction(string actionName, bool runImmediately){return false;}
    public static bool RunActionById(string actionId, bool runImmediately){return false;}
    public static void DisableAction(string actionName){}
    public static void EnableAction(string actionName){}
    public static bool ActionExists(string actionName){return false;}
    public static void LogInfo(string logLine){}
    public static void LogWarn(string logLine){}
    public static void LogError(string logLine){}
    public static void LogDebug(string logLine){}
    public static void LogVerbose(string logLine){}
    public static void DisableTimer(string timerName){}
    public static void EnableTimer(string timerName){}
}