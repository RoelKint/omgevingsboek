var data = [
  { Naam: "Boek naam"},
  { Naam: "Boek"},
  { Naam: "Lololol"}
];

var CommentBox = React.createClass({
  render: function() {
    return (
        <div className="col-sm-6 col-md-3">
            <a href="#" style={{backgroundImage: "url('http://lorempixel.com/350/150/city/')"}}>
                <span className="caption">{this.props.naam}</span>
            </a>
        </div>
    );
  }
});

var CommentList = React.createClass({
  render: function() {
    var commentNodes = this.props.data.map(function (boek) {
      return (
        <CommentBox naam={boek.Naam} />
      );
    });
    return (
		<div className="row books">
			<h3 className="rowheader">Mijn Boeken</h3>
			{commentNodes}
        </div>
    );
  }
});