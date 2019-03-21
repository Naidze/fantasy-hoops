import React, { Component } from 'react';
import _ from 'lodash';
import shortid from 'shortid';
import InjuryCard from './InjuryCard';
import { Loader } from '../Loader';
import { EmptyJordan } from '../EmptyJordan';

export class InjuriesFeed extends Component {
  _isMounted = false;

  constructor(props) {
    super(props);
    this.showModal = this.showModal.bind(this);

    this.state = {
      injuries: [],
      injuryLoader: true
    };
  }

  async componentDidMount() {
    this._isMounted = true;

    await fetch(`${process.env.REACT_APP_SERVER_NAME}/api/injuries`)
      .then(res => res.json())
      .then((res) => {
        if (this._isMounted) {
          this.setState({
            injuries: res,
            injuryLoader: false
          });
        }
      });
    const cards = document.querySelectorAll('.InjuryCard');

    function transition() {
      if (this.classList.contains('active')) {
        this.classList.remove('active');
      } else {
        this.classList.add('active');
      }
    }
    cards.forEach(card => card.addEventListener('click', transition));
  }

  render() {
    const { injuries, injuryLoader } = this.state;
    if (injuryLoader) {
      return (
        <div className="m-5">
          <Loader show={injuryLoader} />
        </div>
      );
    }

    if (injuries.length === 0) {
      return (
        <div className="p-5">
          <EmptyJordan message="No injuries report today..." />
        </div>
      );
    }

    const injuryCards = _.map(injuries, injury => (
      <InjuryCard
        key={shortid()}
        injury={injury}
        showModal={this.showModal}
      />
    ));
    return (
      <div className="mt-3 container bg-light">
        <div className="row">{injuryCards}</div>
      </div>
    );
  }
}

export default InjuriesFeed;
