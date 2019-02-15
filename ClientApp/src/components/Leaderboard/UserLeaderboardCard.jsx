import React, { Component } from 'react';
import Img from 'react-image';
import defaultPhoto from '../../content/images/default.png';

export class UserLeaderboardCard extends Component {
  render() {
    const img = new Image();
    let avatar;
    if (this.props.userid) {
      img.src = `http://fantasyhoops.org/content/images/avatars/${this.props.userid}.png`;
      avatar = img.height !== 0 ? img.src : defaultPhoto;
    }
    return (
      <div className="card bg-white rounded mt-1 mx-auto" style={{ width: '20rem', height: '4.5rem' }}>
        <div className="card-body">
          <div className="d-inline-block align-middle mr-1">
            <h4>{this.props.index + 1}</h4>
          </div>
          <a href={`/profile/${this.props.userName}`} >
            <div className="d-inline-block position-absolute ml-3" style={{ top: '0.2rem' }}>
              <Img
                className="user-card-player"
                alt={this.props.userName}
                src={avatar}
                decode={false}
              />
            </div>
            <div className="d-inline-block">
              <p className="align-middle player-name" style={{ paddingLeft: '5rem', paddingTop: '0.3rem' }}>{this.props.userName}</p>
            </div>
          </a>
          <div className="d-inline-block float-right" style={{ paddingTop: '0.3rem' }}>
            <h5>{Math.round(this.props.fp * 100) / 100} FP</h5>
          </div>
        </div>
      </div>
    );
  }
}