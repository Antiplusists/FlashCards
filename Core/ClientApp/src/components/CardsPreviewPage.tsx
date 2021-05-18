import { Link } from 'react-router-dom';
import { InferProps } from 'prop-types';
import { Link as NavLink, Fab, IconButton, makeStyles } from '@material-ui/core';
import { Edit, Add, DeleteForever } from '@material-ui/icons';
import { FC, useRef, useState } from 'react';
import { CardInfo } from '../types/CardInfo'
import './CardsPreviewPage.css'
import { Sets } from '../fakeSets'
import { Cards } from '../fakeCards'

const useStyles = makeStyles({
  fabOne: {
    margin: 0,
    top: 'auto',
    right: 20,
    bottom: 20,
    left: 'auto',
    position: 'fixed',
  },
  fabTwo: {
    margin: 0,
    top: 'auto',
    right: 90,
    bottom: 20,
    left: 'auto',
    position: 'fixed',
  },
});

type CardPreviewProps = {
  cardInfo: CardInfo,
  onDelete: Function,
}

const CardPreview: FC<CardPreviewProps> = ({ cardInfo, onDelete }) => {
  const { id, questionImg, questionText, answearText } = cardInfo;
  const blockRef = useRef<HTMLDivElement>(null);

  function handleDelete() {
    blockRef.current?.classList.add('isDelete');
    setTimeout(() => onDelete(id), 500);   
  }

  return (
    <div ref={blockRef} className='QAcardPreview'>
      <div className='sideQACard'>
        {questionImg !== undefined ? <img src={questionImg} alt='questionImage' /> : ''}
        <span>{questionText}</span>
      </div>
      <div className='sideQACard'>
        <span>{answearText}</span>
      </div>
      <div className='overlay'>
        <NavLink className='changeButton' component={Link} to={'/card-settings/' + id}>Изменить</NavLink>
        <IconButton className='deleteButton' onClick={handleDelete}>
          <DeleteForever />
        </IconButton>
      </div>
    </div>
  );
}

type СardsPreviewPageProps = {
  setId: number,
}

export default function СardsPreviewPage({ setId }
  : InferProps<СardsPreviewPageProps>) {
  const classes = useStyles();
  const [cardIds, setCardIds] = useState<number[]>(Sets[setId].cardIds);

  function handleAddCard() {
    Sets[setId].cardIds.push(Cards.length);
  }

  function handleRemoveCard(id: number) {
    Sets[setId].cardIds = cardIds.filter(i => i !== id);
    setCardIds(Sets[setId].cardIds);
  }

  return (
    <div>
      <div className='QAcardsPreview'>
        {Sets[setId].cardIds.map(
          id =>
            <CardPreview key={id} cardInfo={Cards[id]} onDelete={handleRemoveCard} />
        )}
      </div>
      <Link to='/card-settings'>
        <Fab className={classes.fabOne} color='primary' onClick={handleAddCard} >
          <Add />
        </Fab>
      </Link>

      <Fab className={classes.fabTwo} color='primary'>
        <Edit />
      </Fab>
    </div>
  );
}