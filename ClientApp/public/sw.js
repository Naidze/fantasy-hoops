'use strict';

importScripts('./js/util.js');

self.addEventListener('install', function (event) {
  self.skipWaiting();
});

self.addEventListener('activate', function (event) {
  event.waitUntil(clients.claim());
});

// Respond to a server push with a user notification
self.addEventListener('push', function (event) {
  if (event.data) {
    const { title, lang = 'en', body, tag, timestamp, requireInteraction, actions, image, icon, data } = event.data.json();
    const promiseChain = self.registration.showNotification(title, {
      lang,
      body,
      requireInteraction,
      tag: tag || undefined,
      timestamp: timestamp ? Date.parse(timestamp) : undefined,
      actions: actions || undefined,
      image: image || undefined,
      icon: icon || './favicon.ico',
      data: data || null,
      vibrate: [200, 100, 200]
    });

    // Ensure the toast notification is displayed before exiting this function
    event.waitUntil(promiseChain);
  }
});

self.addEventListener('notificationclick', async (event) => {
  const model = event.notification.data;
  event.notification.close();
  switch (event.action) {
    case 'accept':
      await fetch('/api/friendrequest/accept', {
        method: 'POST',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(model)
      }).then(async () => {
        const notification = {
          title: "FantasyHoops Friend Request",
          body: `User '${model.receiverUsername}' accepted your friend request!`,
          icon: `https://fantasyhoops.org/content/images/avatars/${model.receiverID}.png`
        };
        await fetch(`./api/push/send/${model.senderID}`, {
          method: 'post',
          headers: {
            'Content-type': 'application/json'
          },
          body: JSON.stringify(notification)
        });
      }).then(clients.openWindow(`/profile/${model.senderUsername}`));
      break;
    case 'decline':
      await fetch('/api/friendrequest/cancel', {
        method: 'POST',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(model)
      })
      break;
    case 'lineup':
      clients.openWindow("/lineup");
      break;
    case 'leaderboard':
      clients.openWindow("/leaderboard/users");
      break;
    default:
      clients.openWindow("/");
  }
}, false);


self.addEventListener('notificationclick', function (event) {
  event.notification.close();

  event.waitUntil(
    clients.matchAll({ type: 'window', includeUncontrolled: true })
      .then(function (clientList) {
        if (clientList.length > 0) {
          let client = clientList[0];

          for (let i = 0; i < clientList.length; i++) {
            if (clientList[i].focused) {
              client = clientList[i];
            }
          }
          return client.focus();
        }

        return clients.openWindow('/');
      })
  );
});

self.addEventListener('pushsubscriptionchange', function (event) {
  event.waitUntil(
    Promise.all([
      Promise.resolve(event.oldSubscription ? deleteSubscription(event.oldSubscription) : true),
      Promise.resolve(event.newSubscription ? event.newSubscription : subscribePush(registration))
        .then(function (sub) { return saveSubscription(sub); })
    ])
  );
});
