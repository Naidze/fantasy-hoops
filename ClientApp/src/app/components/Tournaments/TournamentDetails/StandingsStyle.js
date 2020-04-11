import {makeStyles} from "@material-ui/core/styles";

export const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        height: 'calc(100% - 7rem)'
    },
    container: {
        height: 'calc(100% - 7rem)',
    },
    pagination: {
        display: 'flex',
        flexDirection: 'row',
        flexWrap: 'wrap',
        height: '7rem'
    },
    halfWidth: {
        width: '50%',
        margin: 0
    },
    avatar: {
        width: theme.spacing(3),
        height: theme.spacing(3)
    },
    flexRow: {
        display: 'flex',
        flexDirection: 'row'
    },
    cellValue: {
        margin: '0 1rem'
    }
}));