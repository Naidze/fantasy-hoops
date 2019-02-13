import React, { Component } from 'react';
import { UserScore } from './UserScore';
import { PlayerModal } from '../PlayerModal/PlayerModal';
import shortid from 'shortid';
import { Loader } from '../Loader';
import _ from 'lodash';
const $ = window.$;

export class InfoPanel extends Component {
  constructor(props) {
    super(props);
    this.showModal = this.showModal.bind(this);

    this.state = {
      stats: '',
      modalLoader: true,
      renderChild: true
    }
  }

  componentDidMount() {
    $("#playerModal").on("hidden.bs.modal", () => {
      this.setState({
        modalLoader: true,
        renderChild: false
      });
    });
  }

  async showModal(player) {
    this.setState({ modalLoader: true })
    await fetch(`http://fantasyhoops.org:5001/api/stats/${player.nbaID}`)
      .then(res => res.json())
      .then(res => {
        this.setState({
          stats: res,
          modalLoader: false,
          renderChild: true
        });
      });
  }

  render() {
    const user = this.props.user;
    const recentActivity = () => {
      if (!this.props.loader) {
        const recentActivity = _.map(
          user.recentActivity,
          (activity) => {
            return (
              <UserScore
                key={shortid()}
                posIMG={this.state.posIMG}
                playerIMG={this.state.playerIMG}
                activity={activity}
                showModal={this.showModal}
              />
            )
          });
        return <div>{recentActivity}</div>
      }
      return (
        <div className="p-5">
          <Loader show={this.props.loader} />
        </div>
      )
    }


    return (
      <div className="tab-pane active" id="profile">
        <div className="row">
          <div className="col-md-7">
            <div className="mx-auto mb-3">
              <div className="m-1 badge badge-warning"><i className="fa fa-fire"></i> Streak: {user.streak}</div>
              <a href='/leaderboard/users' className="m-1 badge badge-danger"><i className="fa fa-trophy"></i> Weekly Ranking: {user.position}</a>
              <a href='/leaderboard/users' className="m-1 badge badge-info"><i className="fa fa-basketball-ball"></i> Weekly Score: {Math.round(user.totalScore * 100) / 100} FP</a>
            </div>
            <h5>About</h5>
            <p className='about-me'>
              {user.description}
            </p>
          </div>
          <div className="col-md-4">
            <br />
            <h5 style={{ paddingLeft: '0.8rem' }}>Favorite team</h5>
            <div className="team-badge">
              <h2><span className="badge badge-dark badge-pill"
                style={{ backgroundColor: user !== '' ? user.team.color : '' }}
              >
                {user !== '' ? user.team.name : ''}
              </span></h2>
            </div>
          </div>
          <div className="col-md-12">
            <h5 className="mt-2"><span className="fa fa-clock-o ion-clock"></span> Recent Activity</h5>
            {recentActivity()}
          </div>
        </div>
        <PlayerModal
          renderChild={this.state.renderChild}
          loader={this.state.modalLoader}
          stats={this.state.stats}
        />
      </div>
    );
  }
}
