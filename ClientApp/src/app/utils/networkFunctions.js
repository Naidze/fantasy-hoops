import axios from 'axios';
import _ from 'lodash';

const apiUrlBase = `${process.env.REACT_APP_SERVER_NAME}/api`;

const userApiUrlBase = `${apiUrlBase}/user`;
const friendRequestApiUrlBase = `${apiUrlBase}/friendrequest`;
const playerApiUrlBase = `${apiUrlBase}/player`;
const lineupApiUrlBase = `${apiUrlBase}/lineup`;
const leaderboardApiUrlBase = `${apiUrlBase}/leaderboard`;
const statsApiUrlBase = `${apiUrlBase}/stats`;
const notificationsApiUrlBase = `${apiUrlBase}/notification`;
const pushNotificationsApiUrlBase = `${apiUrlBase}/push`;
const blogApiUrlBase = `${apiUrlBase}/blog`;

const createParameters = (parameters) => {
  if (parameters === undefined) return '';
  if (_.isEmpty(parameters)) return '';
  const strings = _.reduce(parameters, (result, value, key) => {
    if (value && key) return [...result, (`${key}=${value}`)];
    return result;
  }, []);
  return `?${_.join(strings, '&')}`;
};

// User requests
export const register = data => axios.post(`${userApiUrlBase}/register`, data);
export const login = data => axios.post(`${userApiUrlBase}/login`, data);
export const logout = () => {
  fetch(`${userApiUrlBase}/logout`, {
    method: 'GET',
    headers: {
      Authorization: `Bearer ${localStorage.getItem('accessToken')}`
    }
  });
  localStorage.removeItem('accessToken');
  window.location.replace('/');
};
export const editProfile = data => axios.put(`${userApiUrlBase}/editProfile`, data);
export const uploadAvatar = data => axios.post(`${userApiUrlBase}/uploadAvatar`, data);
export const clearAvatar = data => axios.post(`${userApiUrlBase}/clearAvatar`, data);
export const getUsers = () => axios.get(`${userApiUrlBase}`);
export const getUserData = (userId, parameters) => axios.get(`${userApiUrlBase}/${userId}${createParameters(parameters)}`);
export const getUserDataByName = (userName, parameters) => axios.get(`${userApiUrlBase}/name/${userName}${createParameters(parameters)}`);
export const getUserFriends = userId => axios.get(`${userApiUrlBase}/friends/${userId}`);

// Friend requests
export const getUserFriendRequests = userId => axios.get(`${friendRequestApiUrlBase}/requests/${userId}`);
export const updateFriendRequestStatus = data => axios.post(`${friendRequestApiUrlBase}/status`, data);
export const sendFriendRequest = data => axios.post(`${friendRequestApiUrlBase}/send`, data);
export const acceptFriendRequest = data => axios.post(`${friendRequestApiUrlBase}/accept`, data);
export const cancelFriendRequest = data => axios.post(`${friendRequestApiUrlBase}/cancel`, data);
export const removeFriendRequest = data => axios.post(`${friendRequestApiUrlBase}/remove`, data);

// Injuries requests
export const getInjuries = () => axios.get(`${apiUrlBase}/injuries`);

// Players requests
export const getPlayers = () => axios.get(`${playerApiUrlBase}`);
export const getPlayer = playerId => axios.get(`${playerApiUrlBase}/${playerId}`);

// Team requests
export const getTeams = () => axios.get(`${apiUrlBase}/team`);

// Stats requests
export const getStats = () => axios.get(`${statsApiUrlBase}`);
export const getPlayerStats = (playerId, parameters) => axios.get(`${statsApiUrlBase}/${playerId}${createParameters(parameters)}`);

// Leaderboard requests
export const getPlayersLeaderboard = parameters => axios.get(`${leaderboardApiUrlBase}/player${createParameters(parameters)}`);
export const getUsersLeaderboard = parameters => axios.get(`${leaderboardApiUrlBase}/user${createParameters(parameters)}`);
export const getUserFriendsOnlyLeaderboard = (userId, parameters) => axios.get(`${leaderboardApiUrlBase}/user/${userId}${createParameters(parameters)}`);
export const getSeasonPlayersLeaderboard = parameters => axios.get(`${leaderboardApiUrlBase}/season/players${createParameters(parameters)}`);
export const getSeasonLineupsLeaderboard = parameters => axios.get(`${leaderboardApiUrlBase}/season/lineups${createParameters(parameters)}`);
export const getSelectedPlayersLeaderboard = parameters => axios.get(`${leaderboardApiUrlBase}/selected/players${createParameters(parameters)}`);

// News requests
export const getNews = parameters => axios.get(`${apiUrlBase}/news${createParameters(parameters)}`);

// Lineup requests
export const getNextGameInfo = () => axios.get(`${lineupApiUrlBase}/nextGame`);
export const getUserLineup = userId => axios.get(`${lineupApiUrlBase}/${userId}`);
export const submitLineup = lineup => axios.post(`${lineupApiUrlBase}/submit`, lineup);

// Notifications requests
export const getUserNotifications = (userId, parameters) => axios.get(`${notificationsApiUrlBase}/${userId}${createParameters(parameters)}`);
export const readNotification = notification => axios.post(`${notificationsApiUrlBase}/read`, notification);
export const readAllNotifications = userId => axios.post(`${notificationsApiUrlBase}/readall/${userId}`);

// Push Notifications requests
export const sendPushNotification = (receiverId, notification) => axios.post(`${pushNotificationsApiUrlBase}/send/${receiverId}`, notification);
export const getPushPublicKey = () => axios.get(`${pushNotificationsApiUrlBase}/vapidpublickey`);

// Blog requests
export const submitPost = post => axios.post(`${blogApiUrlBase}`, post);
export const getPosts = () => axios.get(`${blogApiUrlBase}`);
export const deletePost = id => axios.delete(`${blogApiUrlBase}?id=${id}`);
