package boardgame;

import java.io.IOException;
import java.util.*;

import boardgame.model.*;
import javafx.beans.value.ObservableValue;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Label;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Circle;
import javafx.stage.Stage;

public class BoardGameController {
    /**
     * Handles who will start the game.
     */
    boolean player1_turn;
    boolean win = false;
    Random random = new Random();
    public void firstTurn(){
        if(random.nextInt(2)==0){
            player1_turn = true;
            turn.setText(names.getPlayerTwo()+"'s turn");
        }
        else{
            player1_turn = false;
            turn.setText(names.getPlayerOne()+"'s turn");
        }
    }




    private enum SelectionPhase {
        SELECT_FROM,
        SELECT_TO;

        public SelectionPhase alter() {
            return switch (this) {
                case SELECT_FROM -> SELECT_TO;
                case SELECT_TO -> SELECT_FROM;
            };
        }
    }

    private SelectionPhase selectionPhase = SelectionPhase.SELECT_FROM;

    private final List<Position> selectablePositions = new ArrayList<>();

    private Position selected;

    private final BoardGameModel model = new BoardGameModel();

    @FXML
    private GridPane board;

    /**
     * At the start it will create all we need for the game.
     */
    @FXML
    private void initialize() {
        firstTurn();
        createBoard();
        createPieces();
        setSelectablePositions();
        showSelectablePositions();
    }


    /**
     * Creates the game board.
     */
    private void createBoard() {
        for (int i = 0; i < board.getRowCount(); i++) {
            for (int j = 0; j < board.getColumnCount(); j++) {
                var square = createSquare();
                board.add(square, j, i);
            }
        }
    }

    private StackPane createSquare() {
        var square = new StackPane();
        square.getStyleClass().add("square");
        square.setOnMouseClicked(this::handleMouseClick);
        return square;
    }

    private void createPieces() {
        for (int i = 0; i < model.getPieceCount(); i++) {
            model.positionProperty(i).addListener(this::piecePositionChange);
            var piece = createPiece(Color.valueOf(model.getPieceType(i).name()));
            getSquare(model.getPiecePosition(i)).getChildren().add(piece);
        }
    }

    private Circle createPiece(Color color) {
        var piece = new Circle(50);
        piece.setFill(color);
        return piece;
    }

    @FXML
    private Label turn;

    String p1 = "BLUE";
    String p2 = "RED";
    MenuController names = new MenuController();

    /**
     * Takes us back to the menu.
     */
    @FXML
    private void goMenu(ActionEvent event)throws IOException{
        System.out.println("Loading menu...");
        System.out.println("-------------------------------------------------");
        Stage stage = (Stage) ((Node) event.getSource()).getScene().getWindow();
        Parent root = FXMLLoader.load(Objects.requireNonNull(getClass().getResource("/menu.fxml")));
        stage.setScene(new Scene(root));
        stage.show();
    }

    /**
     * Handles the mouse click on the squares.
     */
    @FXML
    private void handleMouseClick(MouseEvent event){

        if(!win){
        var square = (StackPane) event.getSource();
        var row = GridPane.getRowIndex(square);
        var col = GridPane.getColumnIndex(square);
        var position = new Position(row, col);
        handleClickOnSquare(position);}
        }

    /**
     * Checks if we moved or not.
     * @param model The piece we clicked on.
     * @param pos The piece position.
     * @return Return if we moved or not.
     */
    boolean isMoved(BoardGameModel model, Position pos){
            for(var cPos:model.getPiecePositions())
            if( cPos.equals(pos) ) {
                return true;}
        return false;
    }

    private void handleClickOnSquare(Position position) {
        switch (selectionPhase) {
            case SELECT_FROM -> {
                if (selectablePositions.contains(position)) {
                    selectPosition(position);
                    alterSelectionPhase();
                }
            }
            case SELECT_TO -> {
                if (isMoved(model, position)){
                    System.out.println("CURRENT POSITION: " + position);
                    System.out.println("-------------------------------------------------");
                    player1_turn = !player1_turn;
                    createPieces();}

                model.getPiecePositions().clear();
                if (selectablePositions.contains(position)) {
                    var pieceNumber = model.getPieceNumber(selected).getAsInt();
                    var direction = PawnDirection.of(position.row() - selected.row(), position.col() - selected.col());
                    model.move(pieceNumber, direction);
                    deselectSelectedPosition();
                    alterSelectionPhase();
                }
            }
        }
    }

    /**
     * Calls the functions we need after selected pos.
     */
    private void alterSelectionPhase() {
        selectionPhase = selectionPhase.alter();
        hideSelectablePositions();
        setSelectablePositions();
        showSelectablePositions();
    }

    private void selectPosition(Position position) {
        selected = position;
        showSelectedPosition();
    }

    private void showSelectedPosition() {
        var square = getSquare(selected);
        square.getStyleClass().add("selected");
    }

    private void deselectSelectedPosition() {
        hideSelectedPosition();
        selected = null;
    }

    private void hideSelectedPosition() {
        var square = getSquare(selected);
        square.getStyleClass().remove("selected");
    }

    /**
     * Chooses the selectable positions for us.
     */
    private void setSelectablePositions() {
        selectablePositions.clear();
        switch (selectionPhase) {
            case SELECT_FROM -> {
                if(player1_turn){
                for (int i = 0; i < 8; i++) {
                    if (model.getPieceType(i) == PieceType.RED)
                    {selectablePositions.add(model.getPiecePosition(i));
                    }
                }
                player1_turn = false;
                }
                else{
                    for (int i = 0; i < 8; i++) {
                        if (model.getPieceType(i) == PieceType.BLUE)
                        {selectablePositions.add(model.getPiecePosition(i));
                        }
                    }
                    player1_turn = true;
                }
            }
            case SELECT_TO -> {
                var pieceNumber = model.getPieceNumber(selected).getAsInt();
                for (var direction : model.getValidMoves(pieceNumber)) {
                    selectablePositions.add(selected.moveTo(direction));
                }
                selectablePositions.add(selected);
            }
        }
    }

    /**
     * Shows the selectable positions.
     */
    private void showSelectablePositions() {
        if(!win){
        for (var selectablePosition : selectablePositions) {
            var square = getSquare(selectablePosition);
            square.getStyleClass().add("selectable");}
        }
    }

    /**
     * Hides the selectable positions.
     */
    private void hideSelectablePositions() {
        for (var selectablePosition : selectablePositions) {
            var square = getSquare(selectablePosition);
            square.getStyleClass().remove("selectable");
        }
    }

    private StackPane getSquare(Position position) {
        for (var child : board.getChildren()) {
            if (GridPane.getRowIndex(child) == position.row() && GridPane.getColumnIndex(child) == position.col()) {
                return (StackPane) child;
            }
        }
        throw new AssertionError();
    }

    /**
     * Changes the piece position.
     */
    private void piecePositionChange(ObservableValue<? extends Position> observable, Position oldPosition, Position newPosition) {
        checksTurn(p1,p2);
        StackPane oldSquare = getSquare(oldPosition);
        StackPane newSquare = getSquare(newPosition);
        newSquare.getChildren().addAll(oldSquare.getChildren());
        oldSquare.getChildren().clear();
        if(!win){
        System.out.println("Moved from: "+ oldPosition +" to "+ newPosition);
        System.out.println("-------------------------------------------------");}
        if(win && player1_turn){
            winnerAlertMSG(names.getPlayerOne());
        }
        else if(win){
            winnerAlertMSG(names.getPlayerTwo());
        }
    }

    /**
     * Pop up an alert if someone won.
     * @param name Player name
     */
    private void winnerAlertMSG(String name){
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("WINNER WINNER CHICKEN DINNER");
        alert.setHeaderText("Congrats "+name +" ,you are the WINNER");
        alert.setContentText("Press OK to save your win.");
        Optional<ButtonType> result = alert.showAndWait();
        int d;
        if(result.isPresent() && result.get() == ButtonType.OK){
            d = names.scoreWin[0]+ 1;
            System.out.println("PLAYER: "+name);
            System.out.println("WINS: "+d);
            System.out.println("-------------------------------------------------");
        }
    }


    /**
     * Checking whose turn is it.
     */
    private void checksTurn(String p1, String p2){
        String player;

        if(player1_turn){
            player = p1;
            positionModelBLUE(player);
            if(!win){
            turn.setText(names.getPlayerTwo()+"'s turn");}}

        else{
            player = p2;
            positionModelRED(player);
            if(!win){
            turn.setText(names.getPlayerOne()+"'s turn");}}
        }

    /**
     * Stores the positions.
     * @param player Player name
     */
    private void positionModelBLUE(String player){
        String[][] game =new String[5][4];
        int row,col;
        System.out.print(player+" --> | ");
        for(int i = 0; i < 4;i++){
            if(model.getPieceType(i) == PieceType.BLUE){
                row = model.getPiecePosition(i).row();
                col = model.getPiecePosition(i).col();
                game[row][col] = player;
                System.out.print("("+row+","+col+ ") | ");
                }}
        System.out.println();
        isGameFinished(game,player, names.getPlayerOne());
    }

    /**
     * Stores the positions.
     * @param player Player name
     */
        private void positionModelRED(String player){
        String[][] game =new String[5][4];
        int row,col;
            System.out.print(player+" --> | ");
            for(int i = 4; i < 8;i++){
                if(model.getPieceType(i) == PieceType.RED){
                    row = model.getPiecePosition(i).row();
                    col = model.getPiecePosition(i).col();
                    game[row][col] = player;
                    System.out.print("("+row+","+col+ ") | ");
                }}
            System.out.println();
        isGameFinished(game,player,names.getPlayerTwo());
    }

    /**
     * Checking that our position is a winning position or not.
     * @param game Positions
     * @param player Player name
     */
    private void isGameFinished(String[][] game, String player, String name){

            if(        (Objects.equals(game[0][0], player) && Objects.equals(game[0][1], player) && Objects.equals(game[0][2], player))
                    || (Objects.equals(game[0][1], player) && Objects.equals(game[0][2], player) && Objects.equals(game[0][3], player))
                    || (Objects.equals(game[1][0], player) && Objects.equals(game[1][1], player) && Objects.equals(game[1][2], player))
                    || (Objects.equals(game[1][1], player) && Objects.equals(game[1][2], player) && Objects.equals(game[1][3], player))
                    || (Objects.equals(game[2][0], player) && Objects.equals(game[2][1], player) && Objects.equals(game[2][2], player))
                    || (Objects.equals(game[2][1], player) && Objects.equals(game[2][2], player) && Objects.equals(game[2][3], player))
                    || (Objects.equals(game[3][0], player) && Objects.equals(game[3][1], player) && Objects.equals(game[3][2], player))
                    || (Objects.equals(game[3][1], player) && Objects.equals(game[3][2], player) && Objects.equals(game[3][3], player))
                    || (Objects.equals(game[4][0], player) && Objects.equals(game[4][1], player) && Objects.equals(game[4][2], player))
                    || (Objects.equals(game[4][1], player) && Objects.equals(game[4][2], player) && Objects.equals(game[4][3], player))

                    || (Objects.equals(game[0][0], player) && Objects.equals(game[1][0], player) && Objects.equals(game[2][0], player))
                    || (Objects.equals(game[1][0], player) && Objects.equals(game[2][0], player) && Objects.equals(game[3][0], player))
                    || (Objects.equals(game[2][0], player) && Objects.equals(game[3][0], player) && Objects.equals(game[4][0], player))
                    || (Objects.equals(game[0][1], player) && Objects.equals(game[1][1], player) && Objects.equals(game[2][1], player))
                    || (Objects.equals(game[1][1], player) && Objects.equals(game[2][1], player) && Objects.equals(game[3][1], player))
                    || (Objects.equals(game[2][1], player) && Objects.equals(game[3][1], player) && Objects.equals(game[4][1], player))
                    || (Objects.equals(game[0][2], player) && Objects.equals(game[1][2], player) && Objects.equals(game[2][2], player))
                    || (Objects.equals(game[1][2], player) && Objects.equals(game[2][2], player) && Objects.equals(game[3][2], player))
                    || (Objects.equals(game[2][2], player) && Objects.equals(game[3][2], player) && Objects.equals(game[4][2], player))
                    || (Objects.equals(game[0][3], player) && Objects.equals(game[1][3], player) && Objects.equals(game[2][3], player))
                    || (Objects.equals(game[1][3], player) && Objects.equals(game[2][3], player) && Objects.equals(game[3][3], player))
                    || (Objects.equals(game[2][3], player) && Objects.equals(game[3][3], player) && Objects.equals(game[4][3], player))

                    || (Objects.equals(game[0][0], player) && Objects.equals(game[1][1], player) && Objects.equals(game[2][2], player))
                    || (Objects.equals(game[1][1], player) && Objects.equals(game[2][2], player) && Objects.equals(game[3][3], player))
                    || (Objects.equals(game[0][2], player) && Objects.equals(game[1][1], player) && Objects.equals(game[2][0], player))
                    || (Objects.equals(game[2][3], player) && Objects.equals(game[3][2], player) && Objects.equals(game[4][1], player))
                    || (Objects.equals(game[0][1], player) && Objects.equals(game[1][2], player) && Objects.equals(game[2][3], player))
                    || (Objects.equals(game[2][0], player) && Objects.equals(game[3][1], player) && Objects.equals(game[4][2], player))
                    || (Objects.equals(game[1][0], player) && Objects.equals(game[2][1], player) && Objects.equals(game[3][2], player))
                    || (Objects.equals(game[2][1], player) && Objects.equals(game[3][2], player) && Objects.equals(game[4][3], player))
                    || (Objects.equals(game[0][3], player) && Objects.equals(game[1][2], player) && Objects.equals(game[2][1], player))
                    || (Objects.equals(game[1][2], player) && Objects.equals(game[2][1], player) && Objects.equals(game[3][0], player))
                    || (Objects.equals(game[1][3], player) && Objects.equals(game[2][2], player) && Objects.equals(game[3][1], player))
                    || (Objects.equals(game[4][0], player) && Objects.equals(game[3][1], player) && Objects.equals(game[2][2], player))
            )
            {
                System.out.println("-------------------------------------------------");
                System.out.println(name +" IS THE WINNER");
                turn.setText(name + " IS THE WINNER!");
                win = true;
            }

    }
    }
