import React, { Component } from 'react';

export class Stats extends Component {
  constructor(props) {
    super(props);
    this.state = {
    }
  }

  render() {
    const stats = this.props.stats;
    let playerImage, opponentLogo;
    try {
      playerImage = require(`../../content/images/players/${stats.nbaID}.png`);
    } catch (err) {
      playerImage = require(`../../content/images/positions/${stats.position.toLowerCase()}.png`);
    }
    try {
      opponentLogo = require(`../../content/images/logos/${stats.team.abbreviation}.svg`);
    } catch (err) {
      opponentLogo = require(`../../content/images/defaultLogo.png`);
    }
    return (
      <div className="row">
        <div style={{ width: '16.25rem', height: '13.05rem' }}></div>
        <img className="img-modal pt-4 mb-2" src={opponentLogo} alt={stats.team.abbreviation} />
        <img className="ml-3 img-modal mb-2" src={playerImage} style={{ zIndex: '1', paddingTop: '1.2rem' }} alt={`${stats.firstName} ${stats.lastName}`} />
        <div className="col">
          <h1 className="">{stats.firstName} {stats.lastName}</h1>
          <h5>{stats.position} | {stats.team.city + " " + stats.team.name}</h5>
          <h5>#{stats.number}</h5>
        </div>
        <div className="table-responsive">
          <table className="table text-right" style={{ maxWidth: '50%' }}>
            <thead>
              <tr>
                <th scope="col" style={{ width: '5rem' }}><h6>PTS</h6><h2>{stats.pts}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>REB</h6><h2>{stats.reb}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>AST</h6><h2>{stats.ast}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>STL</h6><h2>{stats.stl}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>BLK</h6><h2>{stats.blk}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>TOV</h6><h2>{stats.tov}</h2></th>
                <th scope="col" style={{ width: '5rem' }}><h6>FPPG</h6><h2>{stats.fppg.toFixed(1)}</h2></th>
              </tr>
            </thead>
          </table>
        </div>
      </div>
    );
  }
}
