import Injuries from '../constants/injuries';
import { getInjuries } from '../utils/networkFunctions';

export const loadInjuries = () => async (dispatch) => {
  await getInjuries().then((res) => {
    dispatch({
      type: Injuries.GET_INJURIES,
      injuries: res.data,
      injuryLoader: false
    });
  });
};

export default loadInjuries;
