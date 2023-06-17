mergeInto(LibraryManager.library, {
	ShowInterstitialRequest: function () {
		showInterstitial ();
	},
	ShareRequest: function () {
		share ();
	},
	FriendsInviteRequest: function () {
		friendInvite ();
	}
});