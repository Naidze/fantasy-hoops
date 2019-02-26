import React, { Component } from 'react';
import Img from 'react-image';
const $ = window.$;

export class PlayerLeaderboardCard extends Component {
  constructor(props) {
    super(props);
    this.showModal = this.showModal.bind(this);
  }

  componentDidMount() {
    $('[data-toggle="tooltip"]').tooltip()
  }

  showModal() {
    $('[data-toggle="tooltip"]').tooltip("hide");
    this.props.showModal(this.props.player);
  }

  render() {
    return (
      <div className="card bg-white rounded mt-1 mx-auto" style={{ width: '25rem', height: '4.5rem' }}>
        <div className="card-body">
          <div className="row">
            <div className="align-middle text-right" style={{ width: '2rem' }}>
              <h4>{this.props.index + 1}</h4>
            </div>
            <a
              data-toggle="tooltip"
              data-placement="top"
              title="Click for stats"
              style={{ paddingRight: '5rem' }}
            >
              <div
                data-toggle="modal"
                data-target="#playerModal"
                onClick={this.showModal}
                style={{ overflow: 'hidden', cursor: 'pointer' }}>
                <div className="pl-1">
                  <div className="card-circle position-absolute" style={{ top: '0.45rem', backgroundColor: `${this.props.player.teamColor}` }}>
                    <Img
                      className="user-card-player"
                      alt={`${this.props.player.fullName}`}
                      src={[
                        `http://fantasyhoops.org/content/images/players/${this.props.player.nbaID}.png`,
                        this.props.image
                      ]}
                      decode={false}
                    />
                  </div>
                </div>
                <p className="player-name pt-1" style={{ paddingLeft: '5rem' }}>{this.props.player.fullName}</p>
              </div>
            </a>
            <div className="mt-0 position-absolute  " style={{ right: '0rem', width: '8rem' }}>
              <h4 className="text-center">{this.props.player.fp.toFixed(1)} FP</h4>
            </div>
          </div>
        </div>
      </div>
    );
  }
}