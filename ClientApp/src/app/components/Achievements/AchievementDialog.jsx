import React from 'react';
import DialogContent from "@material-ui/core/DialogContent";
import Dialog from "@material-ui/core/Dialog";
import LinearProgress from "@material-ui/core/LinearProgress";
import HighlightOffIcon from '@material-ui/icons/HighlightOff';

import './AchievementDialog.css';
import {useStyles} from "./AchievementDialogStyle";

function AchievementDialog(props) {
    const {achievement, handleClose, open} = props;
    const progressBarValue = achievement.progress / achievement.levelUpGoal * 100;
    const classes = useStyles();
    
    const isSingleLevel = achievement.achievement.type === 0;

    const checkColor = () => {
        return achievement.progress === 0 || (isSingleLevel && !achievement.isAchieved);
    };

    const description = isSingleLevel && achievement.isAchieved
        ? (
            <p className="AchievementDialog__Description">
                {achievement.achievement.completedMessage}
            </p>
        )
        : (

            <p className="AchievementDialog__Description">
                {achievement.achievement.description}
            </p>
        );
    
    const color = checkColor() ? 'no' : '';
    return (
        <Dialog maxWidth="xs" classes={{paper: 'AchievementDialog__Paper'}} onClose={handleClose} aria-labelledby="achievement-dialog" open={open}>
            <HighlightOffIcon className="AchievementDialog__CloseIcon" onClick={handleClose} />
            <DialogContent className={classes.root}>
                <div className="AchievementCard__Icon">
                    <img
                        className="AchievementCard__Icon__Image"
                        alt={achievement.achievement.title}
                        src={require(`../../../content/icons/achievements/${color}color${achievement.achievement.icon}`)}
                    />
                </div>
                <h2 className="AchievementDialog__Title">{achievement.achievement.title}</h2>
                {description}
                <p className="AchievementDialog__Level">
                    LEVEL {achievement.level}
                </p>
                <LinearProgress classes={{root: 'AchievementDialog__ProgressBar'}} variant="determinate" value={progressBarValue}/>
                <p className="AchievementDialog__ProgressNumbers">{`${achievement.progress}/${achievement.levelUpGoal}`}</p>
            </DialogContent>
        </Dialog>
    );
}

export default AchievementDialog;