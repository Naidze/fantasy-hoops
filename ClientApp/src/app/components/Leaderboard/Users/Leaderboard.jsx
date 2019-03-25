import React, { PureComponent } from 'react';
import { Link } from 'react-router-dom';
import shortid from 'shortid';
import _ from 'lodash';
import DatePicker from 'react-datepicker';
import enGB from 'date-fns/locale/en-GB';
import 'react-datepicker/dist/react-datepicker.css';
import moment from 'moment';
import { parse } from '../../../utils/auth';
import Card from './Card';
import leaderboardLogo from '../../../../content/images/leaderboard.png';
import { EmptyJordan } from '../../EmptyJordan';
import { PlayerModal } from '../../PlayerModal/PlayerModal';
import { getUsersLeaderboard, getUserFriendsOnlyLeaderboard, getPlayerStats } from '../../../utils/networkFunctions';

const loggedInUser = parse();
const LOAD_COUNT = 10;
const { $ } = window;

export default class Leaderboard extends PureComponent {
  _isMounted = false;

  constructor(props) {
    super(props);
    this.toggleFriendsOnly = this.toggleFriendsOnly.bind(this);
    this.switchTab = this.switchTab.bind(this);
    this.loadMore = this.loadMore.bind(this);
    this.showModal = this.showModal.bind(this);
    this.onDateChange = this.onDateChange.bind(this);

    this.state = {
      activeTab: 'daily',
      friendsOnly: false,
      daily: [],
      weekly: [],
      monthly: [],
      dailyFriends: [],
      weeklyFriends: [],
      monthlyFriends: [],
      loader: true,
      loadMore: false,
      showButton: {
        daily: false,
        dailyFriends: false,
        weekly: false,
        weeklyFriends: false,
        monthly: false,
        monthlyFriends: false
      },
      stats: '',
      modalLoader: true,
      renderChild: false
    };
  }

  async componentDidMount() {
    $(document).ready(() => {
      $('[data-toggle=tooltip]').tooltip({ trigger: 'hover' });
    });
    $('#playerModal').on('hidden.bs.modal', () => {
      this.setState({
        modalLoader: true,
        renderChild: false
      });
    });
    const { showButton } = this.state;
    await getUsersLeaderboard({ type: 'daily' })
      .then((res) => {
        showButton.daily = res.data.length === LOAD_COUNT;
        this.setState({
          daily: res.data,
          activeTab: 'daily',
          loader: false
        });
      });
    await this.setState({
      PG: require('../../../../content/images/positions/pg.png'),
      SG: require('../../../../content/images/positions/sg.png'),
      SF: require('../../../../content/images/positions/sf.png'),
      PF: require('../../../../content/images/positions/pf.png'),
      C: require('../../../../content/images/positions/c.png')
    });
  }

  async onDateChange(date) {
    this.setState({ loader: true, daily: [], dailyFriends: [] });
    const { friendsOnly, showButton } = this.state;
    const dateFormat = moment(date).format('YYYYMMDD');
    const type = friendsOnly ? 'dailyFriends' : 'daily';

    const users = !friendsOnly
      ? await getUsersLeaderboard({ type: 'daily', date: dateFormat })
      : await getUserFriendsOnlyLeaderboard(loggedInUser.id, { type: 'daily', date: dateFormat });

    showButton[type] = users.data.length === LOAD_COUNT;
    this.setState({
      [type]: users.data,
      loader: false
    });
    this.setState({ date, dateFormat });
  }

  async toggleFriendsOnly() {
    const {
      friendsOnly, activeTab, showButton, dateFormat
    } = this.state;
    this.setState({ friendsOnly: !friendsOnly });
    const type = activeTab + (!friendsOnly ? 'Friends' : '');

    if (this.state[type].length === 0) {
      this.setState({ loader: true });

      const users = friendsOnly
        ? await getUsersLeaderboard({ type: activeTab, date: dateFormat })
        : await getUserFriendsOnlyLeaderboard(loggedInUser.id, { type: activeTab, date: dateFormat });

      showButton[type] = users.data.length === LOAD_COUNT;
      this.setState({
        [type]: users.data,
        loader: false
      });
    }
  }

  async switchTab(e) {
    const {
      friendsOnly, activeTab, showButton, dateFormat
    } = this.state;
    const activeTabURL = e.target.id.split(/-/)[0];
    const type = friendsOnly ? `${activeTabURL}Friends` : activeTabURL;

    if (activeTab === activeTabURL) { return; }

    this.setState({ activeTab: activeTabURL });

    if (this.state[type].length === 0) {
      this.setState({ loader: true });

      const users = !friendsOnly
        ? await getUsersLeaderboard({ type: activeTabURL, date: dateFormat })
        : await getUserFriendsOnlyLeaderboard(loggedInUser.id, { type: activeTabURL, date: dateFormat });

      showButton[type] = users.data.length === LOAD_COUNT;
      this.setState({
        [type]: users.data,
        loader: false
      });
    }
  }

  async loadMore() {
    const {
      activeTab, friendsOnly, showButton, dateFormat
    } = this.state;
    const type = friendsOnly ? `${activeTab}Friends` : activeTab;

    this.setState({ loadMore: true });

    const users = !friendsOnly
      ? await getUsersLeaderboard({ type: activeTab, from: this.state[type].length, limit: LOAD_COUNT, date: dateFormat })
      : await getUserFriendsOnlyLeaderboard(loggedInUser.id, { type: activeTab, from: this.state[type].length, limit: LOAD_COUNT, date: dateFormat });

    showButton[type] = users.data.length === LOAD_COUNT;
    this.setState({
      [type]: this.state[type].concat(users.data),
      loader: false
    });
  }

  seeMoreBtn(type) {
    const { showButton } = this.state;
    return showButton[type] ? <button type="button" className="btn btn-primary mt-2" onClick={this.loadMore}>See more</button> : '';
  }

  async showModal(player) {
    this.setState({ modalLoader: true });
    await getPlayerStats(player.nbaID)
      .then((res) => {
        this.setState({
          stats: res.data,
          modalLoader: false,
          renderChild: true
        });
      });
  }

  createUsers(users, isDaily) {
    return _.map(
      users,
      (user, index) => (
        <Card
          isDaily={isDaily}
          index={index}
          key={shortid()}
          user={user}
          showModal={this.showModal}
        />
      )
    );
  }

  render() {
    const {
      friendsOnly,
      activeTab,
      daily,
      dailyFriends,
      weekly,
      weeklyFriends,
      monthly,
      monthlyFriends,
      loader,
      loadMore,
      renderChild,
      modalLoader,
      stats
    } = this.state;

    const activeType = friendsOnly ? `${activeTab}Friends` : activeTab;
    const dailyUsers = this.createUsers(friendsOnly ? dailyFriends : daily, true);
    const weeklyUsers = this.createUsers(friendsOnly ? weeklyFriends : weekly);
    const monthlyUsers = this.createUsers(friendsOnly ? monthlyFriends : monthly);
    const seeMoreBtn = loader || loadMore
      ? <div className="Loader" />
      : this.seeMoreBtn(activeType);
    return (
      <div className="container bg-light">
        <div className="text-center">
          <img
            src={leaderboardLogo}
            alt="Leaderboard Logo"
            width="60rem"
          />
          <h1>Top Users</h1>
        </div>
        <div className="row justify-content-center">
          <div className="col-xs">
            <div style={{ transform: 'scale(0.7, 0.7)' }}>
              <label className="UserLeaderboard__FriendsOnly">
                <input type="checkbox" checked={friendsOnly} onChange={this.toggleFriendsOnly} />
                <span className="UserLeaderboard__FriendsOnly--slider round" />
              </label>
            </div>
          </div>
          <div className="col-xs pt-2">
            <div>Friends only</div>
          </div>
        </div>
        <ul className="nav nav-pills justify-content-center mx-auto" id="myTab" role="tablist">
          <li className="nav-item">
            <Link className="nav-link active tab-no-outline" id="daily-tab" data-toggle="tab" to="#daily" role="tab" onClick={this.switchTab}>Daily</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link tab-no-outline" id="weekly-tab" data-toggle="tab" to="#weekly" role="tab" onClick={this.switchTab}>Weekly</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link tab-no-outline" id="monthly-tab" data-toggle="tab" to="#monthly" role="tab" onClick={this.switchTab}>Monthly</Link>
          </li>
        </ul>
        <div className="tab-content" id="myTabContent">
          <div className="pt-4 pb-1 tab-pane animated bounceInUp show active" id="daily" role="tabpanel">
            {!this.state.loader
              ? (
                <div className="DatePicker">
                  <DatePicker
                    className="input-group-text"
                    placeholderText="Select the date..."
                    locale={enGB}
                    onChange={this.onDateChange}
                    selected={this.state.date}
                  />
                  <a
                    className="position-absolute"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Works properly only from February 24"
                    style={{ top: '0.3rem', left: '-2rem' }}
                  >
                    <i className="fa fa-info mx-auto" aria-hidden="true" />
                  </a>
                </div>
              )
              : null}
            {!loader
              ? dailyUsers.length > 0
                ? dailyUsers
                : <EmptyJordan message="Such empty..." />
              : ''}
            <div className="text-center">
              {seeMoreBtn}
            </div>
          </div>
          <div className="pt-4 pb-1 tab-pane animated bounceInUp" id="weekly" role="tabpanel">
            {!loader
              ? weeklyUsers.length > 0
                ? weeklyUsers
                : <EmptyJordan message="Such empty..." />
              : ''}
            <div className="text-center">
              {seeMoreBtn}
            </div>
          </div>
          <div className="pt-4 pb-1 tab-pane animated bounceInUp" id="monthly" role="tabpanel">
            {!loader
              ? monthlyUsers.length > 0
                ? monthlyUsers
                : <EmptyJordan message="Such empty..." />
              : ''}
            <div className="text-center">
              {seeMoreBtn}
            </div>
          </div>
        </div>
        <PlayerModal
          renderChild={renderChild}
          loader={modalLoader}
          stats={stats}
        />
      </div>
    );
  }
}
