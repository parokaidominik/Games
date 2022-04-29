package boardgame;

import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.TextField;
import javafx.stage.Stage;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Objects;

public class MenuController {

    @FXML
    private TextField player1;
    @FXML
    private TextField player2;


    public static String p1 = "BLUE";
    public static String p2 = "RED";

    /**
     * Loads the game and starts it.
     * @param event Mouse click on START button.
     */
    @FXML
    private void startGame(ActionEvent event)throws IOException {

        System.out.println("Starting game...");
        Stage stage = (Stage) ((Node) event.getSource()).getScene().getWindow();
        Parent root = FXMLLoader.load(Objects.requireNonNull(getClass().getResource("/game.fxml")));
        stage.setScene(new Scene(root));
        stage.show();
        System.out.println("-------------------------------------------------");
        System.out.println("BLUE PLAYER: "+getPlayerOne());
        System.out.println("RED PLAYER: "+getPlayerTwo());
        System.out.println("-------------------------------------------------");
    }

    /**
     * Saves our added names.
     * @param event Mouse click on SAVE button.
     */
    @FXML
    private void saveNames(ActionEvent event){
        p1 = player1.getText();
        p2 = player2.getText();
        if(p1.equals("")){p1 = "BLUE";}
        if(p2.equals("")){p2 = "RED";}
        System.out.println("-------------------------------------------------");
        System.out.println("Name(s) saved successfully.");
        System.out.println("-------------------------------------------------");
        //saveScores();
    }

    public String[] scoreName = new String[100];
    public int[] scoreWin = new int[100];
    public int wins = 0;
    public int index = 0;
    public void saveScores(){
        scoreName[index] = getPlayerOne();
        scoreWin[index] = wins;
        System.out.println(scoreName[index] +" "+ scoreWin[index]);
    }

    /**
     * Name getters.
     * @return Returns the current player names.
     */
    public String getPlayerOne(){return p1;}
    public String getPlayerTwo(){return p2;}


    /**
     * Exits game.
     * @param event Mouse click on EXIT button.
     */
    @FXML
    private void exitGame(ActionEvent event){
        System.out.println("Exiting....");
        System.out.println("-------------------------------------------------");
        Platform.exit();
    }


}
